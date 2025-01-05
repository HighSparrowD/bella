using Renting.Entities;

namespace Renting.Controls;

public class CostumeSingleView : Panel
{
    private Costume _costume = default!;
    private Action<string> _onButtonClickAction;

    public CostumeSingleView(Costume costume, Action<string> onButtonClickAction, CostumeView parent)
    {
        _costume = costume;
        _onButtonClickAction = onButtonClickAction;

        Width = 400;
        Height = 250;
        BackColor = Color.BlueViolet;
        Name = costume.Name;

        this.Width = 500;
        this.Height = 52;

        var nameLabel = new Label
        {
            Location = new Point(14, 18),
            Width = 45,
            Height = 15,
            Text = _costume.Name
        };

        // It is multiline unlike normal TextBox!
        var descriptiontextBox = new RichTextBox
        {
            Location = new Point(64, 12),
            Width = 250,
            Height = 30,
            Text = _costume.Description,
            Enabled = false
        };

        var rentingTimeLabel = new Label
        {
            Width = 70,
            Height = 15,
            Location = new Point(334, 18)
        };

        if (_costume.RentingTime != null)
        {
            rentingTimeLabel.Text = _costume.RentingTime.Value.ToString("MM-dd-yyyy");
        }

        var isCostumeAvailable = _costume.RentingTime == null;

        var button = new Button
        {
            Location = new Point(415, 14)
        };

        if (isCostumeAvailable)
        {
            button.Text = "Rent";
        }
        else
        {
            button.Text = "Return";
        }

        // s - control that sends the event (EvenSender), e -> variables sent by it (EventArgs)
        button.Click += (s, e) => OnDeleteClick(parent);

        this.Controls.AddRange(new Control[] {nameLabel, descriptiontextBox, rentingTimeLabel, button});
    }

    private void OnDeleteClick(CostumeView parent)
    {
        _onButtonClickAction.Invoke(_costume.Name);
        parent.RemoveCostume(_costume.Name);
        parent.RenderLayout(renderSize: false);
    }
}
