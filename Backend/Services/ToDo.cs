using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Services
{
    public class ToDo
    {
        private readonly DbSource _dbSource;
        public ToDo(DbSource dbSource)
        {
            _dbSource = dbSource;
        }

        public void AddItem(ToDoItem toDoItem)
        {
            if (toDoItem.Created == null)
            {
                toDoItem.Created = DateTime.UtcNow;
            }
            _dbSource.ToDoItem.Add(toDoItem);
            _dbSource.SaveChanges();
        }

        public List<ToDoItem> GetList()
        {
            var list = _dbSource.ToDoItem.ToList();
            return list;
        }

        public void ToggleItem(int id)
        {
            var item = _dbSource.ToDoItem.FirstOrDefault(e => e.Id == id);
            if (item != null)
            {
                if (item.IsComplete == false)
                {
                    item.IsComplete = true;
                    _dbSource.SaveChanges();
                }
                else
                {
                    item.IsComplete = false;
                    _dbSource.SaveChanges();
                }
            }
        }

        public void DeleteItem(int id)
        {
            var item = _dbSource.ToDoItem.FirstOrDefault(e => e.Id == id);
            if (item != null)
            {
                _dbSource.ToDoItem.Remove(item);
                _dbSource.SaveChanges();
            }
            ;
        }
    }
}