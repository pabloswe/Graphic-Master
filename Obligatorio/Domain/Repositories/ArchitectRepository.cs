﻿using System.Collections.Generic;
using Domain.Entities;
using Domain.Interface;
using System.Data.Entity;
using System.Linq;

namespace Domain.Repositories
{
    public class ArchitectRepository : IArchitectRepository
    {
        public void AddArchitect(Architect architect)
        {
            using (DatabaseContext _context = new DatabaseContext())
            {
                _context.Architects.Add(architect);
                _context.SaveChanges();
            }
        }
        public void ModifyArchitect(Architect architect)
        {
            using (DatabaseContext _context = new DatabaseContext())
            {
                _context.Architects.Attach(architect);
                _context.Entry(architect).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }
        public void DeleteArchitect(Architect architect)
        {
            using (DatabaseContext _context = new DatabaseContext())
            {
                _context.Architects.Attach(architect);
                _context.Architects.Remove(architect);
            }
        }
        public bool ArchitectExists(Architect architect)
        {
            Architect architectToFind = null;
            using (DatabaseContext _context = new DatabaseContext())
            {
                architectToFind = _context.Architects.Where(a => a.Username == architect.Username).FirstOrDefault();
            }
            return architectToFind == null ? true : false;
        }
        public bool ArchitectExistsUserName(string username)
        {
            Architect architectToFind = null;
            using (DatabaseContext _context = new DatabaseContext())
            {
                architectToFind = _context.Architects.Where(a => a.Username == username).FirstOrDefault();
            }
            return architectToFind == null ? true : false;
        }
        public Architect GetArchitect(Architect architectToFind)
        {
            using (DatabaseContext _context = new DatabaseContext())
            {
                return _context.Architects.First(architect => architect.Equals(architectToFind));
            }
        }
        public List<Architect> GetAllArchitects()
        {
            using (DatabaseContext _context = new DatabaseContext())
            {
                return _context.Architects.ToList();
            }
        }
    }
}