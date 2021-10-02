using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MandelbrotFractalApplication.Models
{
    class Logic : ILogic
    {
        private readonly Random rnd = new();

        public async Task<List<DoublePoint>> GetPointsAsync(int count)
        {
            var list = new List<DoublePoint>();
            for (int i = 0; i < count; i++)
            {
                list.Add(new DoublePoint(X: rnd.NextDouble(), Y: rnd.NextDouble()));
            }
            // Simulate some 'long running' work
            await Task.Delay(500);
            return list;
        }
    }
}
