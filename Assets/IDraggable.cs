using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public interface IDraggable
    {
        bool CanDrag { get; set; }
        void BeginDrag();
        void EndDrag();
        void UpdateDrag(); 
    }
}
