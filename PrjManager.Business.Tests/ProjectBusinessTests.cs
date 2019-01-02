using NBench;
using PrjManager.Business;
using PrjManager.Entities;
using PrjManager.Repositories;
using PrjManager.Infrastructure.Logging;
using PrjManager.Business.ServiceRequests;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace PrjManager.Business.Tests
{
    public class ProjectBusinessTests
    {
        public ProjectBusinessTests()
        {
        }

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            //Mapper.Reset();
        }

        [PerfBenchmark(Description = "***** Result for GetAllParentTask *****",
                                                              NumberOfIterations = 2,
                                                              RunMode = RunMode.Throughput,
                                                              TestMode = TestMode.Measurement, SkipWarmups = false)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void BenchMarkGetAllParentTask()
        {
            IEnumerable<ParentTaskViewModel> parentTasks;
            IRepository<ProjectTask> taskRepository = new Repository<ProjectTask>();
            IRepository<ParentTask> parenttaskRepository = new Repository<ParentTask>();
            IParentTaskBusiness taskbusiness = new ParentTaskBusiness(parenttaskRepository);
            IRepository<Project> projectRepository = new Repository<Project>();
            IRepository<User> userRepository = new Repository<User>();
            IProjectBusiness projectBusiness = new ProjectBusiness(projectRepository, userRepository, taskRepository);

            TaskBusiness taskBusiness = new TaskBusiness(taskRepository, taskbusiness, projectBusiness,userRepository);
            parentTasks = taskBusiness.GetAllParentTasks();
            Assert.IsNotNull(parentTasks);
        }

        [PerfBenchmark(Description = "***** Result for GetTasks *****",
                                                           NumberOfIterations = 2,
                                                           RunMode = RunMode.Throughput,
                                                           TestMode = TestMode.Measurement, SkipWarmups = false)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void BenchMarkGetTasks()
        {
            IEnumerable<TaskViewModel> tasks;
            IRepository<ProjectTask> taskRepository = new Repository<ProjectTask>();
            IRepository<ParentTask> parenttaskRepository = new Repository<ParentTask>();
            IParentTaskBusiness taskbusiness = new ParentTaskBusiness(parenttaskRepository);

            IRepository<Project> projectRepository = new Repository<Project>();
            IRepository<User> userRepository = new Repository<User>();
            IProjectBusiness projectBusiness = new ProjectBusiness(projectRepository, userRepository, taskRepository);

            TaskBusiness taskBusiness = new TaskBusiness(taskRepository, taskbusiness, projectBusiness, userRepository);
            tasks = taskBusiness.GetAllTasks();
        }

        [PerfBenchmark(Description = "***** Result for GetTask By ID *****",
                                                          NumberOfIterations = 2,
                                                          RunMode = RunMode.Throughput,
                                                          TestMode = TestMode.Measurement, SkipWarmups = false)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void BenchMarkGetTaskById()
        {
            TaskViewModel task;
            IRepository<ProjectTask> taskRepository = new Repository<ProjectTask>();
            IRepository<ParentTask> parenttaskRepository = new Repository<ParentTask>();
            IParentTaskBusiness taskbusiness = new ParentTaskBusiness(parenttaskRepository);


            IRepository<Project> projectRepository = new Repository<Project>();
            IRepository<User> userRepository = new Repository<User>();
            IProjectBusiness projectBusiness = new ProjectBusiness(projectRepository, userRepository, taskRepository);

            TaskBusiness taskBusiness = new TaskBusiness(taskRepository, taskbusiness, projectBusiness, userRepository);
            task = taskBusiness.GetById(1);
        }


        [PerfBenchmark(Description = "***** Result for AddTask *****",
                                                      NumberOfIterations = 1,
                                                      RunMode = RunMode.Throughput,
                                                      TestMode = TestMode.Measurement, SkipWarmups = false)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 100)]
        public void BenchMarkSaveTask()
        {
            TaskViewModel task = new TaskViewModel
            {
                TaskName = "Add Task from nbench",
                StartDate = Convert.ToDateTime("01/01/2018"),
                ProjectId = 1,
                ProjectName = "Project-1",
                ParentTaskId = 1,
                ParentTaskName = "P1-Task",
                Priority = 15,
                EndDate = Convert.ToDateTime("12/12/2018"),
                TaskId = 0
            };

            IRepository<ProjectTask> taskRepository = new Repository<ProjectTask>();
            IRepository<ParentTask> parenttaskRepository = new Repository<ParentTask>();
            IParentTaskBusiness taskbusiness = new ParentTaskBusiness(parenttaskRepository);

            IRepository<Project> projectRepository = new Repository<Project>();
            IRepository<User> userRepository = new Repository<User>();
            IProjectBusiness projectBusiness = new ProjectBusiness(projectRepository, userRepository, taskRepository);

            TaskBusiness taskBusiness = new TaskBusiness(taskRepository, taskbusiness, projectBusiness, userRepository);
            taskBusiness.Save(task);
        }

        [PerfCleanup]
        public void Cleanup()
        {

        }
    }
}
