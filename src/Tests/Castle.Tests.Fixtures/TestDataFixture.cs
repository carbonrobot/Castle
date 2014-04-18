using System;
using System.Data.Entity;
using System.Linq;
using Castle.Domain;
using Castle.Domain.Data;

namespace Castle.Tests.Fixtures
{
    public class TestDataFixture : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            ProjectsFakeyFakey(context);
            base.Seed(context);
        }

        private void ProjectsFakeyFakey(DataContext context)
        {
            var interactive = new Repository() { Name = "Interactive", Path = "interactive" };
            interactive.Projects.Add(new Project()
            {
                Name = "Quickservice",
                Path = "interactive/trunk/Quickservice/trunk"
            });
            interactive.Projects.Add(new Project()
            {
                Name = "Mobile Landing Pages",
                Path = "interactive/trunk/Mobile Landing Pages"
            });

            var docmgt = new Repository() { Name = "Document Management", Path = "document_management" };
            docmgt.Projects.Add(new Project()
            {
                Name = "Document Web Api",
                Path = "document_management/trunk/web/Web.API"
            });

            context.ProjectGroups.Add(interactive);
            context.ProjectGroups.Add(docmgt);
        }
    }
}