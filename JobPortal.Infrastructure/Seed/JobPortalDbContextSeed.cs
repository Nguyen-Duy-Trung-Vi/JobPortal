using JobPortal.Domain.Entities;
using JobPortal.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Infrastructure.Seed
{
    public static class JobPortalDbContextSeed
    {
        public static async Task SeedAsync(JobPortalDbContext context)
        {
            // Check seed data for Company
            if (!context.Companies.Any())
            {
                var companies = new List<Company>
                {
                    new Company { Name = "TechCorp" },
                    new Company { Name = "DevWorks" }
                };
                await context.Companies.AddRangeAsync(companies);
                await context.SaveChangesAsync();
            }

            // Check seed data for JobPost
            if (!context.JobPosts.Any())
            {
                var jobPosts = new List<JobPost>
                {
                    new JobPost
                    {
                        Title = "Software Developer",
                        Description = "Develop and maintain applications.",
                        PostedAt = DateTime.UtcNow,
                        CompanyId = context.Companies.FirstOrDefault(c => c.Name == "TechCorp")?.Id ?? 1
                    },
                    new JobPost
                    {
                        Title = "QA Engineer",
                        Description = "Test software to ensure quality.",
                        PostedAt = DateTime.UtcNow,
                        CompanyId = context.Companies.FirstOrDefault(c => c.Name == "DevWorks")?.Id ?? 2
                    }
                };
                await context.JobPosts.AddRangeAsync(jobPosts);
                await context.SaveChangesAsync();
            }

            // Check seed data for Cadidate
            if (!context.Candidates.Any())
            {
                var candidates = new List<Candidate>
                {
                    new Candidate { FullName = "Nguyen Van A", Email = "ANguyenVan@gmail.com", CreatedAt = DateTime.UtcNow },
                    new Candidate { FullName = "Duong Van B", Email = "BDuongVan@gamil.com", CreatedAt = DateTime.UtcNow }
                };
                await context.Candidates.AddRangeAsync(candidates);
                await context.SaveChangesAsync();
            }

            // Check seed data for JobApplications
            if (!context.JobApplications.Any())
            {
                var jobApplications = new List<JobApplication>
                {
                    new JobApplication
                    {
                        AppliedAt = DateTime.UtcNow,
                        JobPostId = context.JobPosts.FirstOrDefault(j => j.Title == "Software Developer")?.Id ?? 1,
                        CandidateId = context.Candidates.FirstOrDefault(c => c.FullName == "Nguyen Van A")?.Id ?? 1,
                         ResumeUrl = "CV_NguyenVanA.pdf"
                    },
                    new JobApplication
                    {
                        AppliedAt = DateTime.UtcNow,
                        JobPostId = context.JobPosts.FirstOrDefault(j => j.Title == "QA Engineer")?.Id ?? 2,
                        CandidateId = context.Candidates.FirstOrDefault(c => c.FullName == "Duong Van B")?.Id ?? 2,
                        ResumeUrl = "CV_DuongVanB.pdf"
                    }
                };
                await context.JobApplications.AddRangeAsync(jobApplications);
                await context.SaveChangesAsync();
            }
        }
    }
}
