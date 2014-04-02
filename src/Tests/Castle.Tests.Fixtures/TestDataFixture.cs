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
            var interactive = new ProjectGroup() { Name = "Interactive" };
            interactive.Projects.Add(new Project()
            {
                Name = "Quickservice"
            });
            interactive.Projects.Add(new Project()
            {
                Name = "Mobile Landing Pages"
            });

            var poladmin = new ProjectGroup() { Name = "Policy Admin" };
            interactive.Projects.Add(new Project()
            {
                Name = "Phoenix"
            });
            interactive.Projects.Add(new Project()
            {
                Name = "Billing Web Api"
            });

            var docmgt = new ProjectGroup() { Name = "Document Management" };
            interactive.Projects.Add(new Project()
            {
                Name = "Ucm Content System"
            });
            interactive.Projects.Add(new Project()
            {
                Name = "Document Web Api"
            });

            var bi = new ProjectGroup() { Name = "Business Intelligence" };
            interactive.Projects.Add(new Project()
            {
                Name = "Reporting Tools"
            });
            interactive.Projects.Add(new Project()
            {
                Name = "Financial Database"
            });

        }
    }
}