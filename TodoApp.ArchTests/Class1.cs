namespace TodoApp.ArchTests;

public class Class1
{
    [Test]
    public async Task Test2()
    {
        await Task.Delay(1000);
        var a = 1 + 1;
        await Assert
            .That(a)
            .IsEqualTo(2);
    }
}