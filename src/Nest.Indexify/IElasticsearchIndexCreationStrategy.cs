using System.Threading.Tasks;
using Nest.Indexify.Contributors;

namespace Nest.Indexify
{
	public interface IElasticsearchIndexCreationStrategy
    {
        IElasticsearchIndexCreationStrategyResult Create(params IElasticsearchIndexContributor[] additionalContributors);
	    Task<IElasticsearchIndexCreationStrategyResult> CreateAsync(params IElasticsearchIndexContributor[] additionalContributors);
    }
}