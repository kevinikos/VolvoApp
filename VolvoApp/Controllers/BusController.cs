using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace VolvoApp.Controllers
{
    public class BusController : ApiController
    {
        // GET api/bus
        public IEnumerable<Bus> Get()
        {
            using (VolvoDbEntities db = new VolvoDbEntities())
            {
                return db.Buses.ToList();
            }
        }

        // GET /api/bus/5
        public HttpResponseMessage Get(int id)
        {
            using (VolvoDbEntities db = new VolvoDbEntities())
            {
                var bus = db.Buses.Where(x => x.Id == id).FirstOrDefault();
                if (bus != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, bus);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Bus with id " + id + " does not exist");
                }
            }
        }

        // POST api/bus
        public HttpResponseMessage Post([FromBody] Bus bus)
        {
            try
            {
                using (VolvoDbEntities db = new VolvoDbEntities())
                {
                    db.Buses.Add(bus);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Created, bus);

                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        // DELETE api/bus/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (VolvoDbEntities db = new VolvoDbEntities())
                {
                    db.Buses.Remove(db.Buses.FirstOrDefault(x => x.Id == id));
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "Record has been successfully deleted");
                }
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bus with id " + id + " does not exist");
            }
        }

        // PUT
        public HttpResponseMessage Put(int id, [FromBody] Bus bus)
        {
            try
            {
                using (VolvoDbEntities db = new VolvoDbEntities())
                {
                    var entity = db.Buses.FirstOrDefault(x => x.Id == id);

                    entity.VinNumber = bus.VinNumber;
                    entity.Model = bus.Model;
                    entity.Color = bus.Color;
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "Record has been successfully updated");
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }           
    }
}