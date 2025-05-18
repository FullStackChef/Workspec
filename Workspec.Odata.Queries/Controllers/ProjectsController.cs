using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Workspec.Contracts.Queries;
using Workspec.Contracts.Queries;

namespace Workspec.Odata.Queries.Controllers
{
    public abstract class BaseOdataController<T> : ODataController where T : class
    {
        [EnableQuery]
        public virtual IActionResult Get([FromServices] OdataQueryContext dataQueryContext)
        {
            var results = dataQueryContext.Set<T>();
            return Ok(results);
        }
        [EnableQuery(MaxExpansionDepth = 5, MaxAnyAllExpressionDepth = 5)]
        public virtual IActionResult Get([FromServices] OdataQueryContext dataQueryContext, int key)
        {
            var result = dataQueryContext.Set<T>().Find(key);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
    public class ProjectsController : BaseOdataController<ProjectQueryModel>
    {
        [HttpGet("odata/Projects(Path={path})")]
        [EnableQuery(MaxExpansionDepth = 5, MaxAnyAllExpressionDepth = 5)]
        public IActionResult GetProjectByPath([FromServices] OdataQueryContext dataQueryContext, string path)
        {
            var result = dataQueryContext
                .Set<ProjectQueryModel>()
                .SingleOrDefault(p => p.Path == path);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
    public class ProductsController : BaseOdataController<ProductQueryModel>;
    public class ModulesController : BaseOdataController<ModuleQueryModel>;
    public class FeaturesController : BaseOdataController<FeatureQueryModel>;
}
