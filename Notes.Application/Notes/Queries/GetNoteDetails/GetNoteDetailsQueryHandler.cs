using System;
using Notes.Application.Interfaces;
using Notes.Domain;
using Notes.Application.Common;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Notes.Application.Notes.Queries.GetNoteDetails
{
    public class GetNoteDetailsQueryHandler : IRequestHandler<GetNoteDetailsQuery, NoteDetailVm>
    {
        private readonly INotesDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetNoteDetailsQueryHandler(INotesDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<NoteDetailVm> Handle(GetNoteDetailsQuery request, CancellationToken cancellationToken)
        {
            var foundNote = await _dbContext.Notes.FirstOrDefaultAsync(el => el.Id == request.Id, cancellationToken);
            if (foundNote == null || foundNote.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }
            return _mapper.Map<NoteDetailVm>(foundNote);
        }
    }
}
