using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPro.Shared.Models.DataVisualizer
{
    public class EntryVisualizerList
    {
        public Exercise Exercise { get; set; }
        public List<EntryVisualizer> List { get; set;} = new List<EntryVisualizer>();
    }
}
