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
            var interactive = new Repository() { Name = "Interactive" };
            interactive.Projects.Add(new Project()
            {
                Name = "Quickservice"
            });
            interactive.Projects.Add(new Project()
            {
                Name = "Mobile Landing Pages"
            });

            var poladmin = new Repository() { Name = "Policy Admin" };
            poladmin.Projects.Add(new Project()
            {
                Name = "Phoenix"
            });
            poladmin.Projects.Add(new Project()
            {
                Name = "Billing Web Api"
            });

            var docmgt = new Repository() { Name = "Document Management" };
            docmgt.Projects.Add(new Project()
            {
                Name = "Ucm Content System"
            });
            docmgt.Projects.Add(new Project()
            {
                Name = "Document Web Api"
            });

            var bi = new Repository() { Name = "Business Intelligence" };
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