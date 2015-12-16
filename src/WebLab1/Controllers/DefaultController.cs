using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebLab1.Controllers
{
    public class Member
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class DefaultController : ApiController
    {
        List<Member> _Members = new List<Member>(new Member[] {
            new Member { ID = 1, Name = "Bruce S" },
            new Member { ID = 2, Name = "Cindy K"},
            new Member { ID = 3, Name = "Michael J"}
        });

        public IEnumerable<Member> Get()
        {
            return _Members;
        }

        public Member Get(int id)
        {
            return _Members.FirstOrDefault(m => m.ID == id);
        }

        public HttpResponseMessage Post(Member newMember)
        {
            _Members.Add(newMember);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.StatusCode = HttpStatusCode.Created;//201
            string uri = Url.Link("DefaultApi", new {id = newMember.ID });//Related to route setting.
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public HttpResponseMessage Put(int id, Member member)
        {
            var updateMember = Get(id);
            updateMember.Name = member.Name;
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, updateMember);
            string uri = Url.Link("DefaultApi", new { id = id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public HttpResponseMessage Delete(int id)
        {
            _Members.Remove(Get(id));
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NoContent);//204
            return response;
        }
    }
}
