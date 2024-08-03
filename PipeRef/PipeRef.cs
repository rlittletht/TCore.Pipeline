using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Xml;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TCore.Pipeline;

namespace PipeRef
{
    public partial class PipeRef : Form
    {
        // A lot of the code in this is to show some practical uses as 
        // well as demonstrate the multithreaded behavior.

        // in reality, very little is required to use this.
        class WorkCost
        {
            public string Name { get; set; }
            public int WorkMsecCost { get; set; }

            public WorkCost(string name, int cost)
            {
                Name = name;
                WorkMsecCost = cost;
            }

            public override string ToString()
            {
                return Name;
            }
        }

        private WorkCost[] m_costs = new[]
                                     {
                                         new WorkCost("None", 0),
                                         new WorkCost("1 ms", 1),
                                         new WorkCost("5 ms", 5),
                                         new WorkCost("10 ms", 10),
                                         new WorkCost("100 ms", 100),
                                         new WorkCost("1 s", 1000),
                                         new WorkCost("5 s", 5000),
                                         new WorkCost("10 s", 10000)
                                     };

        public PipeRef()
        {
            InitializeComponent();
            m_cbxCost.Items.Clear();
            foreach (WorkCost cost in m_costs)
            {
                m_cbxCost.Items.Add(cost);
            }
        }

        private void DoCreatePipeline(object sender, EventArgs e)
        {
            Log($"{DateTime.Now}: Creating Pipeline");
            pipeline = new ProducerConsumer<WorkItem>(null, ProcessWorkItems);
            pipeline.Start();
        }

        private int m_workId = 0;
        private ProducerConsumer<WorkItem> pipeline;

        private void DoAdd1(object sender, EventArgs e)
        {
            WorkItem work = new WorkItem(m_workId++, m_msecCost, m_allowQueueAbort.Checked);
            Log($"{DateTime.Now}: Adding WorkId({work.WorkId})");
            pipeline.Producer.QueueRecord(work);
        }

        private void DoAdd5(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                WorkItem work = new WorkItem(m_workId++, m_msecCost, m_allowQueueAbort.Checked);
                Log($"{DateTime.Now}: Adding WorkId({work.WorkId})");
                pipeline.Producer.QueueRecord(work);
            }
        }

        private int m_msecCost = 0;

        private void DoChangeWorkCost(object sender, EventArgs e)
        {
            m_msecCost = ((WorkCost)m_cbxCost.SelectedItem).WorkMsecCost;
        }

        private void DoTerminatePipeline(object sender, EventArgs e)
        {
            Log($"{DateTime.Now}: Requesting terminate...");

            ThreadPool.QueueUserWorkItem(
                (_) =>
                {
                    pipeline.Stop();
                    Application.Exit();
                });
        }
    }
}
