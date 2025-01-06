namespace Maraton_api.Dto
{
    public record CreateFutoDto(int Fid, string Fnev, int Szulev, int Szulho, int Csapat, bool Ffi);
    public record UpdateFutoDto(string Fnev, int Szulev, int Szulho, int Csapat, bool Ffi);

    public record UpdateEredmenyekDto
    {
        public int Ido { get; set; }
        public int Kor { get; set; }
        public int Futo { get; set; } 
    }
}
