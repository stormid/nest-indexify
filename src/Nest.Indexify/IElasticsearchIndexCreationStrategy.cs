using System.Threading.Tasks;
using Nest.Indexify.Contributors;

namespace Nest.Indexify
{
	public interface IElasticsearchIndexCreationStrategy
    {
		void Create(params IElasticsearchIndexCreationContributor[] additionalContributors);
	    Task CreateAsync(params IElasticsearchIndexCreationContributor[] additionalContributors);
    }
}