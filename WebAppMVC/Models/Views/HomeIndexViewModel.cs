using WebAppMVC.Models.Components;
using WebAppMVC.Models.Sections;

namespace WebAppMVC.Models.Views;

public class HomeIndexViewModel
{
    public string Title { get; set; } = "Ultimate Task Mangement Assistant";
    public ShowcaseViewModel Showcase { get; set; } = new ShowcaseViewModel
    {
        Id = "overview",
        ShowcaseImage = new() { ImageUrl = "Images/showcase-taskmaster.svg", AltText = "Task Mangement Assistant" },
        Title = "Task Management Assistant You Gonna Love",
        Text = "We offer you a new generation of task management system. Plan, manage & track all your tasks in one flexible tool.",
        Link = new() { ControllerName = "Downloads", ActionName = "Index", LinkText = "Get started for free" },
        BrandsText = "Largest companies use our tool to work efficiently",
        Brands =
            [

                new() { ImageUrl = "Images/brands/brand_1.svg", AltText = "Brand name 1" },
                new() { ImageUrl = "Images/brands/brand_2.svg", AltText = "Brand name 2" },
                new() { ImageUrl = "Images/brands/brand_3.svg", AltText = "Brand name 3" },
                new() { ImageUrl = "Images/brands/brand_4.svg", AltText = "Brand name 4" },
            ]
    };

    public FeaturesViewModel Features { get; set; } = new FeaturesViewModel
    {
        Id = "features",
        Title = "What Do You Get With Our Tool?",
        Text = "Make sure all your tasks are organized so you can set the priorities and focus on important.",
        Tools =
            [

                new FeatureItemViewModel { ImageUrl = "Images/icons/chat.svg", AltText = "Tools picture 1", ToolTitle = "Comment on Tasks", ToolText = "Id mollis consecteur congue egestas egestas suspendisse blandit justo." },
                new FeatureItemViewModel { ImageUrl = "Images/icons/presentation.svg", AltText = "Tools picture 2", ToolTitle = "Task Analytics", ToolText = "Non imperdiet facilisis nulla tellus Morbi scelerisque eget adipiscing vulputate." },
                new FeatureItemViewModel { ImageUrl = "Images/icons/add-group.svg", AltText = "Tools picture 3", ToolTitle = "Multiple Assignees", ToolText = "A elementum, imperdiet enim, pretium etiam facilisi in aenean quam mauris." },
                new FeatureItemViewModel { ImageUrl = "Images/icons/bell.svg", AltText = "Tools picture 4", ToolTitle = "Notifications", ToolText = "Diam, suspendisse velit cras ac. Lobortis diam volutpat, eget pellentesque viverra." },
                new FeatureItemViewModel { ImageUrl = "Images/icons/tests.svg", AltText = "Tools picture 5", ToolTitle = "Sections & Subtasks", ToolText = "Mi feugiat hac id in. Sit elit placerat lacus nibh lorem ridiculus lectus." },
                new FeatureItemViewModel { ImageUrl = "Images/icons/shield.svg", AltText = "Tools picture 6", ToolTitle = "Data Security", ToolText = "Aliquam malesuada neque eget elit nulla vestibulum nunc cras." }
            ],
    };
}
