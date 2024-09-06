using System;
using System.Threading.Tasks;
using MediatR;
using Notes.Domain;
using Notes.Application.Interfaces;
using System.Threading;

namespace Notes.Application.Notes.Commands.CreateNote
{
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
    {
        private readonly INotesDbContext _dbContext;
        public CreateNoteCommandHandler(INotesDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateNoteCommand request,
            CancellationToken cancellationToken)
        {
            var note = new Note()
            {
                UserId = request.UserId,
                Title = request.Title,
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                EditDate = null
            };

            await _dbContext.Notes.AddAsync(note);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return note.Id;
        }
    }
}
