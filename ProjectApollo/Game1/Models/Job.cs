using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApollo
{
    public enum JobType
    {
        Building
    }

    public class Job
    {
        public JobType jobType;
        public Tile tile;
        public Furniture furniturePrototype;
        public Furniture owner;
        public Entity worker;

        public float jobTime, jobTimeRequiered;
        public bool buildAdjacent;

        // Events
        public Action<Job> OnJobCompleted;
        public Action<Job> OnJobStopped;

        public Job(JobType jobType, Tile tile, Furniture furniturePrototype, float jobTime, bool buildAdjacent, Entity worker, Furniture owner = null)
        {
            this.jobType = jobType;
            this.tile = tile;
            this.furniturePrototype = furniturePrototype;
            this.jobTime = jobTime = jobTimeRequiered;
            this.buildAdjacent = buildAdjacent;
            this.owner = owner;

            this.OnJobCompleted = (job) =>
            {
                WorldController.instance.world.SetTile(tile.X, tile.Y, Tiles.GetTile("brick"));

                job.worker.currentJob = null;
                Debug.WriteLine("Job has been completed");
            };
        }

        public void DoWork(float workTime)
        {
            jobTime -= workTime;

            if (workTime <= 0)
            {
                OnJobCompleted?.Invoke(this);
            }
        }

    }
}
