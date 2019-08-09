using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RiderDataAccess;
using System.Data.Entity;
using System.Net.Http;
using System.Net;
using System;
using System.Diagnostics;

namespace API_KeyNinja.Controllers
{
    public class jobsController : ApiController
    {
        // GET api/jobs
        public HttpResponseMessage Get(string type="all")
        {
            using (RidersDetailsEntities entities = new RidersDetailsEntities())
            {
                switch (type)
                {
                    case "average":
                        var groupJobs = entities.jobs.GroupBy(t => new { Id = t.Rider_Id })
                            .Select(g => new
                            {
                                Average = g.Average(p => p.Review_Score),
                                ID = g.Key.Id, Max = g.Max(p => p.Review_Score), BestComment = g.Max(p => p.Review_Comment)
                            });
                        var query =
                                    from riders in entities.Rider
                                    join job in groupJobs on riders.Id equals job.ID
                                    select new { Jobs = job, Riders = riders };
                        return Request.CreateResponse(HttpStatusCode.OK, query.ToList());
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.jobs.ToList()); 
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Page Not Found");
            }
            //  return null;
        }

        [HttpGet]
        public HttpResponseMessage getJobsById(int id)
        {
            using (RidersDetailsEntities entities = new RidersDetailsEntities())
            {
                var entity = entities.jobs.FirstOrDefault(e => e.Id == id);

                if(entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Jobs with id = "+ id.ToString() + "not found");
                }
            }
        }

        // POST api/jobs
        public HttpResponseMessage Post([FromBody] jobs jobs)
        {
            try
            {
                using (RidersDetailsEntities entities = new RidersDetailsEntities())
                {
                    entities.jobs.Add(jobs);
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, jobs);
                    message.Headers.Location = new Uri(Request.RequestUri + jobs.Id.ToString());
                    return message;

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        // PUT api/jobs/5
        public HttpResponseMessage Put(int id, [FromBody]jobs job)
        {
            try
            {
                using (RidersDetailsEntities entities = new RidersDetailsEntities())
                {
                    var entity = entities.jobs.FirstOrDefault(e => e.Id == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Jobs with id = " + id.ToString() + "not found");
                    }
                    else
                    {
                        entity.Date_Time = job.Date_Time;
                        entity.Rider_Id = job.Rider_Id;
                        entity.Review_Score = job.Review_Score;
                        entity.Review_Comment = job.Review_Comment;
                        entity.Completed_At = job.Completed_At;
                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // DELETE api/jobs/5
        public HttpResponseMessage Delete(int id)
        {
            using (RidersDetailsEntities entities = new RidersDetailsEntities())
            {
                var entity = entities.jobs.FirstOrDefault(e => e.Id == id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Jobs with id = " + id.ToString() + "not found");
                }
                else
                {
                    entities.jobs.Remove(entity);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
        }
    }
}