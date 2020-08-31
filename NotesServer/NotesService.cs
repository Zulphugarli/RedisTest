using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using NotesServer.DTO;
using ServiceStack.ServiceInterface;

namespace NotesServer
{
    class NotesService : Service
    {
        private Dictionary<int, string> notes = new Dictionary<int, string>() {
                                                        { 1, "Hello" },
                                                        { 2, "World" } };

        public object Any(NoteRequestDTO request)
        {
            if (!request.Id.HasValue)
            {
                var notesResponse = from note in notes
                                        select new NoteResponseDTO {
                                            Note = note.Value
                                        };

                return notesResponse.ToList();
            }

            if (notes.ContainsKey(request.Id.Value))
            {
                return new NoteResponseDTO { Note = notes[request.Id.Value] };
            }
            else
                return string.Empty;
        }
    }
}
