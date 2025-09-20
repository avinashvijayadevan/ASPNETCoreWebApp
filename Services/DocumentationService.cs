using ASPNETCoreWebApp.Models.Documentation;
using System.Collections.Immutable;

namespace ASPNETCoreWebApp.Services;

public class DocumentationService : IDocumentationService
{
    private readonly IReadOnlyList<Book> _books;

    public DocumentationService()
    {
        _books = BuildLibrary();
    }

    public IReadOnlyList<Book> GetBooks() => _books;

    public Book? GetBookBySlug(string slug)
    {
        return _books.FirstOrDefault(book =>
            string.Equals(book.Slug, slug, StringComparison.OrdinalIgnoreCase));
    }

    public Chapter? GetChapter(string bookSlug, string chapterSlug)
    {
        var (chapter, _, _) = GetChapterWithSiblings(bookSlug, chapterSlug);
        return chapter;
    }

    public (Chapter? chapter, Chapter? previous, Chapter? next) GetChapterWithSiblings(string bookSlug, string chapterSlug)
    {
        var book = GetBookBySlug(bookSlug);
        if (book is null)
        {
            return (null, null, null);
        }

        var orderedChapters = book.Chapters
            .OrderBy(chapter => chapter.Order)
            .ThenBy(chapter => chapter.Title)
            .ToList();

        var index = orderedChapters.FindIndex(chapter =>
            string.Equals(chapter.Slug, chapterSlug, StringComparison.OrdinalIgnoreCase));

        if (index < 0)
        {
            return (null, null, null);
        }

        var chapter = orderedChapters[index];
        var previous = index > 0 ? orderedChapters[index - 1] : null;
        var next = index < orderedChapters.Count - 1 ? orderedChapters[index + 1] : null;

        return (chapter, previous, next);
    }

    private static IReadOnlyList<Book> BuildLibrary()
    {
        var productGuide = new Book
        {
            Title = "GitBook Lite — Product Guide",
            Slug = "product-guide",
            Description = "Everything you need to create, organize and publish beautiful documentation spaces.",
            Icon = "🚀",
            AccentColor = "#6366F1",
            LastUpdated = new DateTimeOffset(2023, 9, 18, 0, 0, 0, TimeSpan.Zero),
            Tags = new[] { "Getting started", "Authors", "Spaces" },
            Chapters = new List<Chapter>
            {
                new()
                {
                    Title = "Welcome to your space",
                    Slug = "welcome-to-your-space",
                    Summary = "Understand how spaces, navigation and publishing work together in GitBook Lite.",
                    Order = 1,
                    EstimatedReadMinutes = 6,
                    Sections = new List<ChapterSection>
                    {
                        new()
                        {
                            Anchor = "why-gitbook-lite",
                            Title = "Why teams love GitBook",
                            Paragraphs = new[]
                            {
                                "GitBook Lite reimagines documentation as a collaborative canvas. Organize knowledge in spaces, invite team members, and publish polished guides in minutes.",
                                "Spaces keep everything structured. Pages, nested groups and callouts mirror the layout your readers expect without forcing you into a rigid tree." ,
                            },
                            KeyPoints = new[]
                            {
                                "Structure information with nested navigation and callouts.",
                                "Publish updates instantly or schedule them for later.",
                                "Invite reviewers with granular commenting access.",
                            },
                            CalloutTitle = "Tip",
                            CalloutBody = "Start with a single space dedicated to your product. You can link to additional playbooks later without disrupting readers.",
                            CalloutTone = "success",
                        },
                        new()
                        {
                            Anchor = "space-anatomy",
                            Title = "Anatomy of a space",
                            Paragraphs = new[]
                            {
                                "A space combines your navigation, content and publishing preferences. Drafts live alongside published pages, so contributors collaborate without risking the live version.",
                                "Within each space you can define collections to group related documentation sets like onboarding, API references and troubleshooting." ,
                            },
                            KeyPoints = new[]
                            {
                                "Navigation groups order your chapters and sections.",
                                "Collections allow multi-product organizations to reuse templates.",
                                "Integrations connect spaces with Slack, Jira and the tools you already use.",
                            },
                        },
                        new()
                        {
                            Anchor = "publishing-flow",
                            Title = "Publishing flow",
                            Paragraphs = new[]
                            {
                                "Every page keeps a draft and published state. Draft changes are private to editors until you publish them.",
                                "Use review requests to loop in subject matter experts. Approvals are tracked directly on the page so the audit trail lives beside the content." ,
                            },
                            CalloutTitle = "Remember",
                            CalloutBody = "Publishing pushes only the pages you approve. Drafts for other chapters stay untouched, making iterative updates simple.",
                            CalloutTone = "info",
                        },
                    },
                },
                new()
                {
                    Title = "Authoring content",
                    Slug = "authoring-content",
                    Summary = "Master the editor, reusable blocks and collaboration tools.",
                    Order = 2,
                    EstimatedReadMinutes = 8,
                    Sections = new List<ChapterSection>
                    {
                        new()
                        {
                            Anchor = "editor-essentials",
                            Title = "Editor essentials",
                            Paragraphs = new[]
                            {
                                "Compose pages with headings, callouts, tabs and embeds. Markdown shortcuts keep writers in flow while block menus surface advanced formatting.",
                                "Drag and drop blocks to reorganize sections. Multi-select allows large structural updates without losing context." ,
                            },
                            KeyPoints = new[]
                            {
                                "Type '/' to open the block palette.",
                                "Use nested callouts to highlight warnings or success states.",
                            },
                        },
                        new()
                        {
                            Anchor = "collaboration",
                            Title = "Collaboration",
                            Paragraphs = new[]
                            {
                                "Comment on specific blocks to ask questions, request context or highlight wins.",
                                "Mention teammates with @ to bring them into the conversation and assign tasks.",
                            },
                            CalloutTitle = "Pro tip",
                            CalloutBody = "Enable change suggestions to allow reviewers to propose edits that you can accept with one click.",
                            CalloutTone = "success",
                        },
                        new()
                        {
                            Anchor = "content-reuse",
                            Title = "Reuse content",
                            Paragraphs = new[]
                            {
                                "Save recurring snippets as reusable blocks. Marketing updates, release highlights or troubleshooting steps stay consistent across pages.",
                                "Version content across spaces by syncing pages. GitBook keeps track of updates and suggests when linked spaces need a refresh.",
                            },
                        },
                    },
                },
                new()
                {
                    Title = "Publishing and analytics",
                    Slug = "publishing-and-analytics",
                    Summary = "Share documentation with the right audience and learn what resonates.",
                    Order = 3,
                    EstimatedReadMinutes = 7,
                    Sections = new List<ChapterSection>
                    {
                        new()
                        {
                            Anchor = "share-controls",
                            Title = "Share controls",
                            Paragraphs = new[]
                            {
                                "Set your space visibility to public, private or invite-only. Share single pages with review links when you need quick feedback.",
                                "Custom domains and themes keep your docs consistent with your brand, so public readers feel at home.",
                            },
                        },
                        new()
                        {
                            Anchor = "insights",
                            Title = "Insights and feedback",
                            Paragraphs = new[]
                            {
                                "Track how readers navigate your documentation with built-in analytics. Identify chapters that spark questions and ship improvements fast.",
                                "Collect emoji reactions and inline feedback so your team knows which topics deserve more attention.",
                            },
                            CalloutTitle = "Next steps",
                            CalloutBody = "Connect your space with Slack or email to notify the team when readers leave feedback.",
                            CalloutTone = "warning",
                        },
                    },
                },
            }
        };

        var onboardingPlaybook = new Book
        {
            Title = "Engineering Playbook",
            Slug = "engineering-playbook",
            Description = "A curated onboarding space for new engineers joining the team.",
            Icon = "🧭",
            AccentColor = "#10B981",
            LastUpdated = new DateTimeOffset(2023, 10, 2, 0, 0, 0, TimeSpan.Zero),
            Tags = new[] { "Onboarding", "Culture", "Best practices" },
            Chapters = new List<Chapter>
            {
                new()
                {
                    Title = "Week 1 roadmap",
                    Slug = "week-1-roadmap",
                    Summary = "Set newcomers up for success with a clear schedule and goals.",
                    Order = 1,
                    EstimatedReadMinutes = 5,
                    Sections = new List<ChapterSection>
                    {
                        new()
                        {
                            Anchor = "day-one",
                            Title = "Day one essentials",
                            Paragraphs = new[]
                            {
                                "Kick off with a welcome call, workspace setup and access provisioning. Pair new hires with a buddy who can answer questions quickly.",
                                "Point engineers to the product guide so they can explore the platform from a reader's perspective.",
                            },
                            KeyPoints = new[]
                            {
                                "Provide system access before the first day.",
                                "Share the product space as required reading.",
                            },
                        },
                        new()
                        {
                            Anchor = "learning-plan",
                            Title = "Craft a learning plan",
                            Paragraphs = new[]
                            {
                                "Encourage engineers to journal their first week observations directly in GitBook Lite. Inline comments become a feedback loop for documentation gaps.",
                                "Outline shadowing sessions, pair programming slots and product deep-dives so expectations stay clear.",
                            },
                        },
                    },
                },
                new()
                {
                    Title = "Ways of working",
                    Slug = "ways-of-working",
                    Summary = "Document rituals, code review etiquette and communication norms.",
                    Order = 2,
                    EstimatedReadMinutes = 6,
                    Sections = new List<ChapterSection>
                    {
                        new()
                        {
                            Anchor = "rituals",
                            Title = "Team rituals",
                            Paragraphs = new[]
                            {
                                "Stand-ups happen asynchronously inside GitBook comments. Engineers post blockers and updates before 10am local time.",
                                "Sprint reviews focus on outcomes. Embed loom videos, prototypes and doc links so stakeholders can catch up at their own pace.",
                            },
                        },
                        new()
                        {
                            Anchor = "quality-bar",
                            Title = "Quality bar",
                            Paragraphs = new[]
                            {
                                "All pull requests include context, screenshots and links to the relevant documentation chapter.",
                                "Use the troubleshooting chapter when customer issues arise. Leave reaction emojis when fixes are shipped to close the loop.",
                            },
                            CalloutTitle = "Team value",
                            CalloutBody = "Docs are the source of truth. If a process changes, update the space before announcing it in chat.",
                            CalloutTone = "info",
                        },
                    },
                },
                new()
                {
                    Title = "Tooling index",
                    Slug = "tooling-index",
                    Summary = "Quick links and onboarding tasks for the platforms we rely on daily.",
                    Order = 3,
                    EstimatedReadMinutes = 4,
                    Sections = new List<ChapterSection>
                    {
                        new()
                        {
                            Anchor = "repositories",
                            Title = "Repositories",
                            Paragraphs = new[]
                            {
                                "Our code lives on GitHub. Bookmark the engineering playbook repository and subscribe to release notes.",
                                "Use GitBook's Git sync integration if you prefer to draft long-form guides in your IDE.",
                            },
                        },
                        new()
                        {
                            Anchor = "communications",
                            Title = "Communication channels",
                            Paragraphs = new[]
                            {
                                "Slack remains our heartbeat. Join #onboarding, #release-updates and your squad channel.",
                                "We mirror major announcements inside GitBook so knowledge stays discoverable beyond the chat timeline.",
                            },
                        },
                    },
                },
            }
        };

        return ImmutableList.Create(productGuide, onboardingPlaybook);
    }
}
