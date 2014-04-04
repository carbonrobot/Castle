using System;
using System.Data.Entity;
using System.Linq;
using Castle.Domain;
using Castle.Domain.Data;

namespace Castle.Tests.Fixtures
{
    public class TestDataFixture : DropCreateDatabaseAlways<DataContext>
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
                Path = "interactive/quickservice"
            });
            interactive.Projects.Add(new Project()
            {
                Name = "Mobile Landing Pages",
                Path = "interactive/mobile landing pages"
            });

            var poladmin = new Repository() { Name = "Travel", Path = "travel" };
            poladmin.Projects.Add(new Project()
            {
                Name = "Trip Defender"
            });
            poladmin.Projects.Add(new Project()
            {
                Name = "Billing Web Api"
            });

            var docmgt = new Repository() { Name = "Document Management", Path = "document_management" };
            docmgt.Projects.Add(new Project()
            {
                Name = "Ucm Content System"
            });
            docmgt.Projects.Add(new Project()
            {
                Name = "Document Web Api"
            });

            var bi = new Repository() { Name = "Architecture", Path = "architecture" };
            bi.Projects.Add(new Project()
            {
                Name = "Reporting Tools"
            });
            bi.Projects.Add(new Project()
            {
                Name = "Financial Database"
            });

            context.ProjectGroups.Add(interactive);
            context.ProjectGroups.Add(poladmin);
            context.ProjectGroups.Add(docmgt);
            context.ProjectGroups.Add(bi);
        }
    }
}