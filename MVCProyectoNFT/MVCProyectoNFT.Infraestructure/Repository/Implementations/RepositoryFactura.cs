using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVCProyectoNFT.Infraestructure.Data;
using MVCProyectoNFT.Infraestructure.Models;
using MVCProyectoNFT.Infraestructure.Repository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProyectoNFT.Infraestructure.Repository.Implementations
{
    public class RepositoryFactura : IRepositoryFactura
    {
        private readonly ProyectoNFTContext _context;
        private readonly ILogger<RepositoryFactura> _logger;
        public RepositoryFactura(ProyectoNFTContext context, ILogger<RepositoryFactura> logger)
        {
            _context = context;
            _logger = logger;

        }

        public async Task<int> AddAsync(FacturaEncabezado entity)
        {
            try
            {
                // Get No Receipt
                entity.Id = GetNoReceipt();
                // Reenumerate
                entity.FacturaDetalle.ToList().ForEach(p => p.IdFactura = entity.Id);
                // Begin Transaction
                await _context.Database.BeginTransactionAsync();
                await _context.Set<FacturaEncabezado>().AddAsync(entity);

                // Withdraw from inventory
                //foreach (var item in entity.FacturaDetalle)
                //{
                //    // find the product
                //    var nft = _context.Set<Nft>().Find(item.IdNft);
                //    // update stock
                    
                //    // update entity product
                //    _context.Set<Nft>().Update(nft);
                //}

                await _context.SaveChangesAsync();
                // Commit
                await _context.Database.CommitTransactionAsync();

                foreach (var item in entity.FacturaDetalle)
                {
                    var nft = _context.Set<Nft>().Find(item.IdNft);

                    await AddClienteNFT(nft.Id, (int)entity.IdCliente, DateTime.Now, true, entity.Id, nft.Nombre);
                }

                return entity.Id;
            }
            catch (Exception ex)
            {
                Exception exception = ex;

                _logger.LogError(ex, "Error AddAsync");
                // Rollback 
                await _context.Database.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task AddClienteNFT(string idNft, int idCliente, DateTime fecha, bool estado, int idFactura, string nombre)
        {
            // Raw Query
            //https://www.learnentityframeworkcore.com/raw-sql/execute-sql
            int rowAffected = _context.Database.ExecuteSqlInterpolated($"INSERT INTO ClienteNFT (idCliente, idNFT, Fecha, Estado, idFactura, nombreNFT) VALUES ({idCliente}, {idNft}, {fecha}, {true}, {idFactura}, {nombre})");
            await Task.FromResult(1);

        }

        /// <summary>
        /// Get sequence in order to assign Receipt number.   
        /// Automaticaly INCREMENT ++
        /// http://technet.microsoft.com/es-es/library/ff878091.aspx
        /// CREATE SEQUENCE  ReceiptNumber  START WITH 1 INCREMENT BY 1 ;
        /// </summary>
        /// <returns>Num. de factura</returns>

        private int GetNoReceipt()
        {
            var list = _context.Database.SqlQueryRaw<Int64>($"SELECT NEXT VALUE FOR ReceiptNumber").ToList();
            var next = list[0];
            return (int)next;
        }
        [Obsolete("No utilizar")]
        private int GetNoReceiptOLD()
        {
            int siguiente = 0;

            string sql = string.Format("SELECT NEXT VALUE FOR ReceiptNumber");

            System.Data.DataTable dataTable = new System.Data.DataTable();

            System.Data.Common.DbConnection connection = _context.Database.GetDbConnection();
            System.Data.Common.DbProviderFactory dbFactory = System.Data.Common.DbProviderFactories.GetFactory(connection!)!;
            using (var cmd = dbFactory!.CreateCommand())
            {
                cmd!.Connection = connection;
                cmd.CommandText = sql;
                using (System.Data.Common.DbDataAdapter adapter = dbFactory.CreateDataAdapter()!)
                {
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dataTable);
                }
            }


            siguiente = Convert.ToInt32(dataTable.Rows[0][0].ToString());
            return siguiente;

        }

        public async Task<ICollection<FacturaEncabezado>> BillsByClientIdAsync(int id)
        {
            var response = await _context.Set<FacturaEncabezado>()
                           .Include(p => p.FacturaDetalle)
                           .Where(p => p.IdCliente == id).ToListAsync();

            return response;
        }

        public async Task<FacturaEncabezado> FindByIdAsync(int id)
        {
            var response = await _context.Set<FacturaEncabezado>()
                        .Include(detalle => detalle.FacturaDetalle)
                        .ThenInclude(detalle => detalle.IdNftNavigation)
                        .Include(cliente => cliente.IdClienteNavigation)
                        .Where(p => p.Id == id).FirstOrDefaultAsync();

            return response!;
        }

        public async Task<ICollection<FacturaEncabezado>> ListAsync()
        {
            var collection = await _context.Set<FacturaEncabezado>().Include(b => b.IdClienteNavigation).Include(b => b.IdTipoTarjetaNavigation).Include(b => b.FacturaDetalle).AsNoTracking().ToListAsync();

            var listaDueno = collection.Where(item => item.Estado == true)
                                 .ToList();

            return listaDueno!;
        }

        /// <summary>
        /// Get current NoReceipt without increment
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetNextReceiptNumber()
        {

            int current = 0;

            string sql = string.Format("SELECT current_value FROM sys.sequences WHERE name = 'ReceiptNumber'");

            System.Data.DataTable dataTable = new System.Data.DataTable();

            System.Data.Common.DbConnection connection = _context.Database.GetDbConnection();
            System.Data.Common.DbProviderFactory dbFactory = System.Data.Common.DbProviderFactories.GetFactory(connection!)!;
            using (var cmd = dbFactory!.CreateCommand())
            {
                cmd!.Connection = connection;
                cmd.CommandText = sql;
                using (System.Data.Common.DbDataAdapter adapter = dbFactory.CreateDataAdapter()!)
                {
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dataTable);
                }
            }


            current = Convert.ToInt32(dataTable.Rows[0][0].ToString());
            return await Task.FromResult(current);

        }

        public async Task Anular(int id)
        {
            // Raw Query
            //https://www.learnentityframeworkcore.com/raw-sql/execute-sql
            int rowAffected = _context.Database.ExecuteSql($"UPDATE ClienteNFT SET Estado = {false} Where IdFactura = {id}");
            int rowAffected2 = _context.Database.ExecuteSql($"UPDATE FacturaEncabezado SET Estado = {false} Where Id = {id}");
            await Task.FromResult(1);

        }
    }
}
