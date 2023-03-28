using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour
{
    //アニメーションするためのコンポーネントを取得
    private Animator myAnimator;
    //Unityちゃんを移動させるコンポーネントを取得
    private Rigidbody myRigidbody;
    //前方向の速度
    private float velocityZ = 16f;
    // 横方向の速度
    private float velocityX = 10f;
    //上方向の速度
    private float velocityY = 10f;
    //左右の移動できる範囲
    private float movableRange = 3.4f;
    //動きを減衰させる係数
    private float coefficient = 0.99f;
    //ゲーム終了時の判定
    private bool isEnd = false;
    //ゲーム終了時に表示するテキスト
    private GameObject stateText;
    //スコアを表示するテキスト
    private GameObject ScoreText;
    //得点
    private int Score = 0;
    //左ボタンを押した場合
    private bool isLButtonDown = false;
    //右ボタンを押した場合
    private bool isRButtonDown = false;
    //ジャンプボタンを押した場合
    private bool isJButtonDown = false;

    // Start is called before the first frame update
    void Start()
    {
        //Animatorコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();
        //走るアニメーションを再生
        this.myAnimator.SetFloat("Speed", 1);
        //Rgidbodyコンポーネントを取得
        this.myRigidbody = GetComponent<Rigidbody>();
        //シーン中にstateTextオブジェクトを取得
        this.stateText = GameObject.Find("GameResultText");
        //シーン中にScoreTextオブジェクトを取得
        this.ScoreText = GameObject.Find("ScoreText");
    }

    // Update is called once per frame
    void Update()
    {
        //ゲーム終了ならUnityちゃんの動きを減衰させる
        if(this.isEnd)
        {
            this.velocityZ *= this.coefficient;
            this.velocityX *= this.coefficient;
            this.velocityY *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;
        }

        //横方向の入力による速度
        float inputvelocityX = 0;
        //上方向の入力による速度
        float inputvelocityY = 0;

        //Unityちゃんを矢印キーまたはボタンに応じて左右に移動する
        if((Input.GetKey(KeyCode.LeftArrow)|| this.isLButtonDown) &&  -this.movableRange< this.transform.position.x)
        {
            //左方向への速度を代入
            inputvelocityX = -this.velocityX;
        }
        else if((Input.GetKey(KeyCode.RightArrow)||this.isRButtonDown) && this.transform.position.x < this.movableRange)
        {
            //右方向への速度を代入
            inputvelocityX = this.velocityX;
        }

        //ジャンプしていない時にスペースが押されたらジャンプする
        if((Input.GetKeyDown(KeyCode.Space)|| this.isJButtonDown) && this.transform.position.y < 0.5f)
        {
            //ジャンプアニメを再生
            this.myAnimator.SetBool("Jump", true);
            //上方向への速度を代入
            inputvelocityY = this.velocityY;
        }
        else
        {
            //現在のY軸の速度を代入
            inputvelocityY = this.myRigidbody.velocity.y;
        }

        //Jumpステートの場合はJumpにFalseをセットする
        if(this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }
        //Unityちゃんに速度を与える
        this.myRigidbody.velocity = new Vector3(inputvelocityX, inputvelocityY, this.velocityZ);
    }

    //トリガーモードで他のオブジェクトに接触した場合の処理
    private void OnTriggerEnter(Collider other)
    {
        //障害物に衝突した場合の処理
        if(other.gameObject.tag =="CarTag" || other.gameObject.tag == "TrafficConeTag")
        {
            this.isEnd = true;
            //stateTextにGAMEOVERを表示
            this.stateText.GetComponent<Text>().text = "Game Over";
        }
        if(other.gameObject.tag == "GoalTag")
        {
            this.isEnd = true;
            //stateTextにGAMEＣＬＥＡＲを表示
            this.stateText.GetComponent<Text>().text = "CLEAR!!";
        }

        //コインに衝突した場合
        if(other.gameObject.tag == "CoinTag")
        {
            //スコアを加算
            this.Score += 10;

            //scoretextに獲得した点数を表示
            this.ScoreText.GetComponent<Text>().text = "Score" + this.Score + "pt";
            //パーティクルを再生
            GetComponent<ParticleSystem>().Play();

            //接触したコインのオブジェクトを破棄
            Destroy(other.gameObject);
        }
              
    }

    //ジャンプボタンを押した場合の処理
    public void GetMyJumpButtonDown()
    {
        this.isJButtonDown = true;
    }
    //ジャンプボタンを離した場合の処理
    public void GerMyJumpButtonUp()
    {
        this.isJButtonDown = false;
    }
    //左ボタンを押した場合
    public void GetMyLeftButtonDown()
    {
        this.isLButtonDown = true;
    }

    public void GerMyLeftButtonUp()
    {
        this.isLButtonDown = false;
    }
    //右ボタンを押した場合
    public void GetMyRightButtonDown()
    {
        this.isRButtonDown = true;
    }

    public void GerMyRightButtonUp()
    {
        this.isRButtonDown = false;
    }
}
