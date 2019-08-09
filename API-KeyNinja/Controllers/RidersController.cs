using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RiderDataAccess;
using System.Data.Entity;
using System.Net.Http;
using System.Net;
using System;
namespace API_KeyNinja.Controllers

{
    public class RidersController : ApiController
    {

        // GET api/riders
        public IEnumerable<Rider> Get()
        {
            using (RidersDetailsEntities entities = new RidersDetailsEntities())
            {
                return entities.Rider.ToList();
            }
              //  return null;
        }

        // GET api/riders/5
        public HttpResponseMessage Get(int id)
        {
            using (RidersDetailsEntities entities = new RidersDetailsEntities())
            {
                var entity = entities.Rider.FirstOrDefault(e => e.Id == id);

                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Rider with id = " + id.ToString() + "not found");
                }
            }
        }

        // POST api/riders
        public HttpResponseMessage Post([FromBody] Rider rider)
        {
            try
            {
                using (RidersDetailsEntities entities = new RidersDetailsEntities())
                {
                    rider.Start_Date = DateTime.Now;
                    entities.Rider.Add(rider);
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, rider);
                    message.Headers.Location = new Uri(Request.RequestUri + rider.Id.ToString());
                    return message;

                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            
        }

        // PUT api/riders/5
        public HttpResponseMessage Put(int id, [FromBody]Rider rider)
        {
            try
            {
                using (RidersDetailsEntities entities = new RidersDetailsEntities())
                {
                    var entity = entities.Rider.FirstOrDefault(e => e.Id == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Rider with id = " + id.ToString() + "not found");
                    }
                    else
                    {
                        entity.FirstName = rider.FirstName;
                        entity.LastName = rider.LastName;
                        entity.Phone_Number = rider.Phone_Number;
                        entity.Email = rider.Email;
                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // DELETE api/riders/5
        public HttpResponseMessage Delete(int id)
        {
            using (RidersDetailsEntities entities = new RidersDetailsEntities())
            {
                var entity = entities.Rider.FirstOrDefault(e => e.Id == id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Rider with id = " + id.ToString() + "not found");
                }
                else
                {
                    entities.Rider.Remove(entity);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                
            }
        }
    }
}
