﻿using WebAppMVC.ViewModels.Components;

namespace WebAppMVC.ViewModels.Sections;

public class ManageWorkViewModel
{
    public string? Id { get; set; }
    public ImageViewModel ManageWork { get; set; } = new ImageViewModel();
    public string? Title { get; set; }
    public List<IconViewModel>? WorkItems { get; set; }
    public LinkViewModel Link { get; set; } = new LinkViewModel();
}
