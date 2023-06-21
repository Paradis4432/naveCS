using Game;

public interface IFisica {
    void CalcularFisica(float dt);
    void AplicarFuerza(Vector2 f);
    void AplicarAceleracion(Vector2 f);
    void AplicarTorque(float t);
    void AplicarFriccion(float dt, float mult);
}