namespace Keepr.Models
{
  public class Vault
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public string UserId { get; set; }
    public int Id { get; set; }
    public class VaultKeepViewModel : Vault
    {
      public int VaultKeepId { get; set; }
    }
  }
}