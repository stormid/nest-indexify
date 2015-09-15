using System.Threading.Tasks;
using Nest.Indexify.Contributors;

namespace Nest.Indexify
{
	public interface IElasticsearchIndexCreationStrategy
    {
		void Create(params IElasticsearchIndexContributor[] additionalContributors);
	    Task CreateAsync(params IElasticsearchIndexContributor[] additionalContributors);
    }
}