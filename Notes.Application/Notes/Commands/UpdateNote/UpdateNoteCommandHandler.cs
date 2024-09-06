using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Notes.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Notes.Domain;
using Notes.Application.Common;

namespace Notes.Application.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, Unit>
    {
        private readonly INotesDbContext _dbContext;
        public UpdateNoteCommandHandler(INotesDbContext dbContext) =>
            _dbContext = dbContext;
        public async Task<Unit> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            Note foundNote = await _dbContext.Notes.FirstOrDefaultAsync(el => el.Id == request.Id, cancellationToken);
            if (foundNote == null || foundNote.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }

            foundNote.Title = request.Title;
            foundNote.Details = request.Details;
            foundNote.EditDate = DateTime.Now;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}