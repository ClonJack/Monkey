using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Monkey : MonoBehaviour
{
    [SerializeField] private Transform partBody;
    [SerializeField] private Camera camera;

    [SerializeField] private bool isStarDance;

    [SerializeField] private Vector3 direction;

    [SerializeField] private bool isAudtoDance;

    [SerializeField] private Brilliant brilliant;
    [SerializeField] private Banana banana;
    [SerializeField] private Body body = new Body();


    private IEnumerator GameDance()
    {
        isAudtoDance = true;
        partBody = body.partMonkeys[0].Part;

        while (body.idPart < body.partMonkeys.Count)
        {
            partBody = body.partMonkeys[body.idPart].Part;

            yield return new WaitForSeconds(1f);
            isStarDance = true;
            if (partBody == null)
            {
                body.sequence.Add(body.partMonkeys[body.idPart].Part);
                isStarDance = true;
                body.idPart++;

            }

        }

        isAudtoDance = false;
        isStarDance = false;
    }

    private bool IsRotatePartBody(PartMonkey monkey)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && monkey != null)
            return true;

        return false;
    }

    private bool IsEnOfMovement(Transform rotate, float limit)
    {
        if (rotate.transform.eulerAngles.z >= limit)
            return true;


        return false;

    }
    private void DoDance()
    {
        if (partBody != null)
        {
            if (!isStarDance) return;

            partBody.rotation = Quaternion.RotateTowards(partBody.rotation, Quaternion.Euler(direction), Time.deltaTime * body.speedMovePart);

            if (IsEnOfMovement(partBody, 39f))
                direction = Vector3.zero;

            if (partBody.rotation.eulerAngles.z <= 0)
            {
                isStarDance = false;
                partBody = null;

            }

        }

        if (!isStarDance && body.sequencePlyer.Count == body.sequence.Count && !isAudtoDance)
        {
            brilliant.rootGm.SetActive(true);
            isAudtoDance = true;

            body.partMonkeys.Add(body.partMonkeys[Random.Range(0, body.partMonkeys.Count - 1)]);

            body.sequencePlyer.Clear();
            body.sequence.Clear();
            body.idPart = 0;
            direction = new Vector3(0, 0, 40);

            StartCoroutine(GameDance());
        }

    }

    private void Getinfo()
    {
        if (isStarDance) return;

        direction = new Vector3(0, 0, 40);
        var point = new Vector2(camera.ScreenToWorldPoint(Input.mousePosition).x, camera.ScreenToWorldPoint(Input.mousePosition).y);

        var hit = Physics2D.Raycast(point, point);
        try
        {
            if (!isAudtoDance)
            {
                if (IsRotatePartBody(hit.transform.GetComponent<PartMonkey>()))
                {
                    partBody = hit.transform.GetComponent<PartMonkey>().Part;

                    body.sequencePlyer.Add(hit.transform.GetComponent<PartMonkey>().Part);

                    if (!(body.sequence[body.sequencePlyer.Count - 1].Equals(body.sequencePlyer[body.sequencePlyer.Count - 1])))
                    {
                        body.sequencePlyer.Clear();
                        body.sequence.Clear();
                        body.idPart = 0;
                        partBody = body.partMonkeys[0].Part;
                        isAudtoDance = true;

                        StartCoroutine(GameDance());

                    }



                    isStarDance = true;


                }
            }

        }
        catch
        {

        }

    }
    private void Start()
    {
        StartCoroutine(GameDance());
    }

    private void Update()
    {

        if (!isAudtoDance && partBody != null)
            banana.RotateBonana();

        Getinfo();
        DoDance();

    }

}
