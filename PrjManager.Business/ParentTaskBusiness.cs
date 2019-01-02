using PrjManager.Business.ServiceRequests;
using PrjManager.Entities;
using PrjManager.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace PrjManager.Business
{
    /// <summary>
    /// Interface IParentTaskBusiness
    /// </summary>
    public interface IParentTaskBusiness
    {
        ParentTaskViewModel Save(ParentTaskViewModel model);
        IEnumerable<ParentTaskViewModel> GetAll();
    }

    /// <summary>
    /// ParentTaskBusiness Class
    /// </summary>
    public class ParentTaskBusiness : IParentTaskBusiness
    {
        /// <summary>
        /// repository variable
        /// </summary>
        readonly IRepository<ParentTask> _parentTaskRepository;

        public ParentTaskBusiness(IRepository<ParentTask> parentTaskRepository)
        {
            _parentTaskRepository = parentTaskRepository;
        }

        /// <summary>
        /// Get All method
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ParentTaskViewModel> GetAll()
        {
            var entities = _parentTaskRepository.GetAll();
            var models = new List<ParentTaskViewModel>();
            entities.ToList().ForEach(p => models.Add(ToModel(p)));

            return models;
        }

        /// <summary>
        /// Save Method  for ParentTask View Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ParentTaskViewModel Save(ParentTaskViewModel model)
        {
            var entity = _parentTaskRepository.GetById(model.ParentTaskId);
            if (entity == null)
            {
                entity = ToEntity(model);
                _parentTaskRepository.Insert(entity);
                model.ParentTaskId = entity.ParentTaskId;
            }
            else
            {
                entity.ParentTaskTitle = model.ParentTaskName;
                _parentTaskRepository.Update(entity);
            }

            return model;
        }

        /// <summary>
        /// To Entity method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private ParentTask ToEntity(ParentTaskViewModel model)
        {
            return new ParentTask
            {
                ParentTaskId = model.ParentTaskId,
                ParentTaskTitle = model.ParentTaskName
            };
        }

        /// <summary>
        /// To Model method
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private ParentTaskViewModel ToModel(ParentTask entity)
        {
            return new ParentTaskViewModel
            {
                ParentTaskId = entity.ParentTaskId,
                ParentTaskName = entity.ParentTaskTitle
            };
        }
    }
}
