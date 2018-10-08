using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proOfficeTask.Helpers;
using proOfficeTask.Models;

namespace proOfficeTask.Controllers
{
    [Route("api/Docs")]
    public class DocumentsController : Controller
    {
        private readonly ProOfficeTaskDBContext _dbContext;

        public DocumentsController(ProOfficeTaskDBContext context)
        {
            _dbContext = context;
        }

        // GET: api/Docs/GetProducts
        /// <summary>
        /// Get all products from the local DB
        /// </summary>
        /// <returns>List of ProductDTOs objects</returns>
        [HttpGet]
        [Route("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            using (_dbContext)
            {
                try
                {
                    var dbResponse = await _dbContext.TblProduct.Select(p => new ProductDTO()
                    {
                        IdProduct = p.IdProduct,
                        ProductName = p.ProductName,
                        SupplierName = p.SupplierName,
                        Url = p.Url,
                        IdDoc = p.TblDocument.FirstOrDefault(d => d.ProductId == p.IdProduct).IdDoc,
                        Type = p.TblDocument.FirstOrDefault(d => d.ProductId == p.IdProduct).Type,
                        Downloaded = p.TblDocument.FirstOrDefault(d => d.ProductId == p.IdProduct).Downloaded.ToString()

                    }).ToListAsync();
                    return Ok(dbResponse);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
        }

        // GET: api/Docs/GetProducts/giud
        /// <summary>
        /// Get product by id from the local DB
        /// </summary>
        /// <returns>ProductDTO object</returns>
        [HttpGet]
        [Route("GetProduct/{id}")]
        public async Task<IActionResult> GetProducts(string id)
        {
            using (_dbContext)
            {
                try
                {
                    var dbResponse = await _dbContext.TblProduct
                        .Select(p => new ProductDTO()
                        {
                            IdProduct = p.IdProduct,
                            ProductName = p.ProductName,
                            SupplierName = p.SupplierName,
                            Url = p.Url,
                            IdDoc = p.TblDocument.FirstOrDefault(d => d.ProductId == p.IdProduct).IdDoc,
                            Type = p.TblDocument.FirstOrDefault(d => d.ProductId == p.IdProduct).Type,
                            Downloaded = p.TblDocument.FirstOrDefault(d => d.ProductId == p.IdProduct).Downloaded.ToString()

                        }).FirstOrDefaultAsync(tblP => (tblP.IdProduct).ToString() == id);

                    return Ok(dbResponse);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
        }

        // GET: api/Docs/GetDoc/{giud}
        /// <summary>
        /// Get document by id from the local DB
        /// </summary>
        /// <returns>DocumentDTO object</returns>
        [HttpGet]
        [Route("GetDocument/{id}")]
        public async Task<IActionResult> GetDocument(string id)
        {
            using (_dbContext)
            {
                try
                {
                    var dbResponse = await _dbContext.TblDocument
                        .Select(d => new DocumentDTO()
                        {
                            IdDoc = d.IdDoc,
                            Type = d.Type,
                            Downloaded = d.Downloaded,
                            BinaryData = d.TblDocumentData.BinaryData
                        }).FirstOrDefaultAsync(tblD => (tblD.IdDoc).ToString() == id);

                    return Ok(dbResponse);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
        }

        // GET: api/Docs/DownloadDoc/{giud}
        /// <summary>
        /// Get document content by product id
        /// </summary>
        /// <param name="id">guid as string, product id</param>
        /// <returns>Content as file</returns>
        [HttpGet("DownloadDoc")]
        public async Task<IActionResult> DownloadDoc(string id)
        {
            using (_dbContext)
            {
                try
                {
                    var tblProduct = await _dbContext.TblProduct
                        .FirstOrDefaultAsync(tblP => (tblP.IdProduct).ToString() == id);

                    var contentType = FilesHelper.GetContentType(tblProduct.Url);
                    var fileName = FilesHelper.GetFileName(tblProduct.Url);
                    MemoryStream content;

                    var tblDocument = await _dbContext.TblDocument.FirstOrDefaultAsync(d => d.ProductId == tblProduct.IdProduct);
                    if (tblDocument == null)
                    {//no downloaded file
                        content = await DownloadHelper.GetContent(tblProduct.Url);
                        TblDocumentData tblData = new TblDocumentData
                        {
                            IdDocument = Guid.NewGuid(),
                            BinaryData = content.ToArray()
                        };

                        tblDocument = new TblDocument
                        {
                            IdDoc = tblData.IdDocument,
                            ProductId = tblProduct.IdProduct,
                            Type = contentType,
                            Downloaded = DateTime.Now,
                            Product = tblProduct,
                            TblDocumentData = tblData
                        };

                        await _dbContext.TblDocument.AddAsync(tblDocument);
                        await _dbContext.TblDocumentData.AddAsync(tblData);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {//previous downloaded file
                        var tblData = await _dbContext.TblDocumentData
                            .FirstOrDefaultAsync(d => d.IdDocument == tblDocument.IdDoc);
                        if ((tblDocument.Downloaded.HasValue)
                            && (tblDocument.Downloaded.Value <= DateTime.Now.AddDays(-3)))
                        {//older than 3 days
                            content = await DownloadHelper.GetContent(tblProduct.Url);
                            
                            tblDocument.Downloaded = DateTime.Now;
                            tblDocument.Type = contentType;

                            if (content.ToArray() != tblData.BinaryData)
                            {//binary content changed
                                tblData.BinaryData = content.ToArray();
                            }

                            await _dbContext.SaveChangesAsync();

                        }
                        else
                        {
                            content = new MemoryStream(tblData.BinaryData);
                        }
                    }

                    return File(content, contentType, fileName);

                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
        }


    }
}
