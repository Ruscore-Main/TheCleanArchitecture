﻿using System;
using Notes.Application.Common.Mappings;
using Notes.Domain;
using AutoMapper;

namespace Notes.Application.Notes.Queries.GetNoteDetails
{
    public class NoteDetailVm : IMapWith<Note>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? EditDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Note, NoteDetailVm>()
                .ForMember(noteVm => noteVm.Id, opt => opt.MapFrom(note => note.Id))
                .ForMember(noteVm => noteVm.Title, opt => opt.MapFrom(note => note.Title))
                .ForMember(noteVm => noteVm.Details, opt => opt.MapFrom(note => note.Details))
                .ForMember(noteVm => noteVm.CreationDate, opt => opt.MapFrom(note => note.CreationDate))
                .ForMember(noteVm => noteVm.EditDate, opt => opt.MapFrom(note => note.EditDate));

        }
    }
}
