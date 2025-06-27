namespace TodoApp.App.Graphics;

public class TopEllipse : IDrawable
{
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.FillColor = Color.FromArgb("#002a81");
        var width = dirtyRect.Width;
        var height = dirtyRect.Height * 2f;

        canvas.FillRoundedRectangle(
            0,
            -dirtyRect.Height,
            width,
            height,
            120);
    }
}