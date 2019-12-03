using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderWrite.Events;


namespace OrderWrite.Models
{
    public abstract class Entity
    {
        private List<IEvents> changes;
        public Entity()
        {
            changes = new List<IEvents>();
        }
        public IEnumerable<IEvents> GetChanges() {
            return changes.AsEnumerable();
        }

        public void ClearChanges() {
            changes.Clear();
        }

        public void apply(IEvents myevent) {
            when(myevent);
            CheckValidity();
            changes.Add(myevent);
        }

        public abstract void CheckValidity();

        public abstract void when(IEvents myevent);




    }
}
