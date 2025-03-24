using System;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data.Cold;

namespace Infrastructure.Repositories;

public class ArchiveRepository(KMLoggerDbContex contex) 
    : BaseRepository<Archive>(contex), IArchiveRepository;
