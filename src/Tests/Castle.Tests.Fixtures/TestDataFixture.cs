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
            var interactive = new Team() { Name = "Interactive", Nickname = "INA" };
            interactive.Projects.Add(new Project()
            {
                Name = "Quickservice"
            });
            interactive.Projects.Add(new Project()
            {
                Name = "Mobile Landing Pages"
            });

            var poladmin = new Team() { Name = "Policy Admin", Nickname = "POL" };
            poladmin.Projects.Add(new Project()
            {
                Name = "Phoenix"
            });
            poladmin.Projects.Add(new Project()
            {
                Name = "Billing Web Api"
            });

            var docmgt = new Team() { Name = "Document Management", Nickname = "DOC" };
            docmgt.Projects.Add(new Project()
            {
                Name = "Ucm Content System"
            });
            docmgt.Projects.Add(new Project()
            {
                Name = "Document Web Api"
            });

            var bi = new Team() { Name = "Business Intelligence", Nickname = "INT" };
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