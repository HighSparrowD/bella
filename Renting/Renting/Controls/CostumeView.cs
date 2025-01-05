using Renting.Entities;

namespace Renting.Controls;

public class CostumeView : Panel
{
    // No need to use private modificator, without it, variable is still private
    private int yInterval = 65;
    private List<Costume> _costumes;
    private Action<string> _onButtonClickAction;

    public CostumeView(List<Costume> costumes, Action<string> onButtonClickAction)
    {
        _costumes = costumes;
        _onButtonClickAction = onButtonClickAction;

        RenderLayout();
    }

    // true - default value for renderSize argument -> if nothing is passed renderSize = true
    public void RenderLayout(bool renderSize = true)
    {
        if (renderSize)
        {
            Width = 550;
            Height = 250;
            BackColor = Color.BurlyWood;
        }

        // Clear old controls
        this.Controls.Clear();

        foreach (var costume in _costumes)
        {
            AddCostume(costume);
        }
    }

    public void AddCostume(Costume costume, bool addToInternalList = false)
    {
        var view = new CostumeSingleView(costume, _onButtonClickAction, this);
        // TODO: Google inline if for C#
        var yLocation = this.Controls.Count == 0 ? 8 : yInterval * this.Controls.Count;
        view.Location = new Point(25, yLocation);

        if (addToInternalList)
            _costumes.Add(costume);

        this.Controls.Add(view);
    }

    public void RemoveCostume(string costumeName)
    {
        var costume = this.Controls.Find(costumeName, true).FirstOrDefault();
        this.Controls.Remove(costume);
        _costumes.RemoveAll(x => Name == costumeName);
    }
}
