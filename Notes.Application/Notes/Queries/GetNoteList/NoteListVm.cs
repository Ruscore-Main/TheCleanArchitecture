using System.Collections.Generic;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    class NoteListVm
    {
        public IList<NoteLookupDto> Notes { get; set; }
    }
}
