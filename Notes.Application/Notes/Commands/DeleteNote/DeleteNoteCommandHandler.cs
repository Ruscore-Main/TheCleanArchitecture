using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;
using Notes.Application.Common;

namespace Notes.Application.Notes.Commands.DeleteNote
{
    class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, Unit>
    {
        private readonly INotesDbContext _dbContext;

        public DeleteNoteCommandHandler(INotesDbContext dbContext) => _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            var foundNote = await _dbContext.Notes.FirstOrDefaultAsync(el => el.Id == request.Id);
            if (foundNote != null || request.UserId != foundNote.UserId)
            {
                throw new NotFoundException(nameof(Notes), request.Id);
            }
            _dbContext.Notes.Remove(foundNote);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
