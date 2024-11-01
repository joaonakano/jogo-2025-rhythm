using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovimentoPersonagemPlano : MonoBehaviour
{
    public float velocidade = 5f; // Velocidade de movimento no plano XZ
    public float forcaPulo = 5f; // Força do pulo
    private Rigidbody rb;
    private bool estaNoChao;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
    }

    void FixedUpdate()
    {
        // Movimentação no plano XZ
        float movimentoHorizontal = Input.GetAxis("Horizontal");
        float movimentoVertical = Input.GetAxis("Vertical");
        Vector3 movimento = new Vector3(movimentoHorizontal, 0, movimentoVertical).normalized * velocidade;
        rb.MovePosition(rb.position + movimento * Time.fixedDeltaTime);

        // Verifica se o jogador pressiona o botão de pulo e se está no chão
        if (Input.GetButtonDown("Jump") && estaNoChao)
        {
            rb.AddForce(Vector3.up * forcaPulo, ForceMode.Impulse);
            estaNoChao = false; // Impede que pule novamente enquanto estiver no ar
        }
    }

    // Verifica se o personagem toca o chão
    private void OnCollisionEnter(Collision collision)
    {
        // Confirma se a colisão é com o chão usando a tag "Chao"
        if (collision.gameObject.CompareTag("Chao"))
        {
            estaNoChao = true;
        }
    }
}
