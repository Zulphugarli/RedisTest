using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.ServiceHost;

namespace NotesServer.DTO
{
    [Route("/notes")]
    [Route("/notes/{Id}")]
    class NoteRequestDTO
    {
        public int? Id { get; set; }
    }
}
