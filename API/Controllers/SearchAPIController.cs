using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using API.Models;

namespace API.Controllers
{
    public class SearchAPIController : ApiController
    {
        private Norges_EnergiEntities db = new Norges_EnergiEntities();

        // GET: api/SearchAPI
        public IQueryable<InfoViewModel> GetInfoViewModels()
        {
            return db.InfoViewModels;
        }

        // GET: api/SearchAPI/5
        [ResponseType(typeof(InfoViewModel))]
        public async Task<IHttpActionResult> GetInfoViewModel(int id)
        {
            InfoViewModel infoViewModel = await db.InfoViewModels.FindAsync(id);
            if (infoViewModel == null)
            {
                return NotFound();
            }

            return Ok(infoViewModel);
        }

        // PUT: api/SearchAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutInfoViewModel(int id, InfoViewModel infoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != infoViewModel.Info_ID)
            {
                return BadRequest();
            }

            db.Entry(infoViewModel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InfoViewModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/SearchAPI
        [ResponseType(typeof(InfoViewModel))]
        public async Task<IHttpActionResult> PostInfoViewModel(InfoViewModel infoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.InfoViewModels.Add(infoViewModel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = infoViewModel.Info_ID }, infoViewModel);
        }

        // DELETE: api/SearchAPI/5
        [ResponseType(typeof(InfoViewModel))]
        public async Task<IHttpActionResult> DeleteInfoViewModel(int id)
        {
            InfoViewModel infoViewModel = await db.InfoViewModels.FindAsync(id);
            if (infoViewModel == null)
            {
                return NotFound();
            }

            db.InfoViewModels.Remove(infoViewModel);
            await db.SaveChangesAsync();

            return Ok(infoViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InfoViewModelExists(int id)
        {
            return db.InfoViewModels.Count(e => e.Info_ID == id) > 0;
        }

        [HttpGet]
        public IQueryable<info> GetInfo()
        {
            var model = from i in db.info
                        select new info()
                        {
                            info_ID = i.info_ID
                        };
            return model;
        }

        [HttpPost]
        [ResponseType(typeof(info))]
        public async Task<IHttpActionResult> PostInfo(info info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.info.Add(info);
            await db.SaveChangesAsync();

            db.Entry(info).Reference(x => x.stage4).Load();

            var model = new InfoViewModel
            {
                Info_ID = info.info_ID,
                Stage4_ID = info.stage4_ID
            };

            return CreatedAtRoute("DefaultApi", new { id = info.info_ID }, model);
        }

        [HttpGet]
        [ResponseType(typeof(InfoViewModel))]
        public async Task<IHttpActionResult> GetSearch(int id)
        {
            var model = await db.info.Include(i => i.stage4).Select(i =>
            new InfoViewModel()
            {
                Info_ID = i.info_ID,
                Stage4_ID = i.stage4_ID
            }).SingleOrDefaultAsync(i => i.Info_ID == id);
            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }
    }
}