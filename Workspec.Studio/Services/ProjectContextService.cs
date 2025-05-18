using System.Reactive.Linq;
using System.Reactive.Subjects;
using Workspec.Contracts.Queries;
using Workspec.Studio.ServiceClients;

namespace Workspec.Studio.Services
{
    public interface IProjectContextService
    {
        IObservable<ProjectQueryModel?> CurrentProject { get; }
        IObservable<ModuleQueryModel?> SelectedModule { get; }
        IObservable<FeatureQueryModel?> SelectedFeature { get; }
        IObservable<UserRequirementQueryModel?> SelectedRequirement { get; }

        Task LoadProjectByPathAsync(string path);
        void SelectModule(ModuleQueryModel module);
        void SelectFeature(FeatureQueryModel feature);
        void SelectRequirement(UserRequirementQueryModel requirement);
    }

    public class ProjectContextService(IProjectServiceClient projectService) : IProjectContextService
    {
        private readonly BehaviorSubject<ProjectQueryModel?> _currentProject = new(null);
        private readonly BehaviorSubject<ModuleQueryModel?> _selectedModule = new(null);
        private readonly BehaviorSubject<FeatureQueryModel?> _selectedFeature = new(null);
        private readonly BehaviorSubject<UserRequirementQueryModel?> _selectedRequirement = new(null);

        public IObservable<ProjectQueryModel?> CurrentProject => _currentProject.AsObservable();
        public IObservable<ModuleQueryModel?> SelectedModule => _selectedModule.AsObservable();
        public IObservable<FeatureQueryModel?> SelectedFeature => _selectedFeature.AsObservable();
        public IObservable<UserRequirementQueryModel?> SelectedRequirement => _selectedRequirement.AsObservable();

        public async Task LoadProjectByPathAsync(string path)
        {
            var current = _currentProject.Value;
            if (current != null && string.Equals(current.Path, path, StringComparison.OrdinalIgnoreCase))
                return; // Already loaded

            var project = await projectService.GetByPathAsync(path);
            _currentProject.OnNext(project);
        }


        public void SelectModule(ModuleQueryModel module)
        {
            _selectedModule.OnNext(module);
            _selectedFeature.OnNext(null);
            _selectedRequirement.OnNext(null);
        }

        public void SelectFeature(FeatureQueryModel feature)
        {
            _selectedFeature.OnNext(feature);
            _selectedRequirement.OnNext(null);
        }

        public void SelectRequirement(UserRequirementQueryModel requirement)
        {
            _selectedRequirement.OnNext(requirement);
        }
    }
}
