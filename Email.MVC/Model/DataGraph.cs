using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Email.MVC.Model
{
    public class DataGraph
    {
        public int Id { get; set; }

        public List<Graph> Nodes { get; set; }


    }

    public class Graph
    {
        public int Id { get; set; }

        public string Target { get; set; }

        public string Distance { get; set; }

        public string Source { get; set; }
    }

    

    
    }


}
