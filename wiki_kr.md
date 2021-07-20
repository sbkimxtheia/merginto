# 0. 시작하기 전에

## 0.1. 주의사항
이 위키는 프로그램의 세부적인 사용법을 제공하기 위해 제작되었습니다.    
만약 어려운 것이 싫고, 그냥 간단히 진행하는 법을 알고 싶으시다면 간단 가이드를 참고해주세요.   
프로그램이 백신 등의 영향으로 가동되지 않을 수 있으니 백신을 잠시 끄시는 것을 추천드립니다.

## 0.2. 프로그램의 용도
해당 프로그램은 복수의 이미지 파일이 포함된 폴더를 폴더 하나당 하나의 PDF로 출력하는 프로그램입니다.   
예외가 있을 수 있지만, 주로 아래의 경우에 사용을 추천드립니다.   
- 다수의 jpg, png 형식 파일들로 이루어진 웹툰, 만화 등을 하나의 PDF로 파일로 병합   
- jpg, png 형태로 이루어진 문서 집합 등을 하나의 PDF 파일로 병합   

여러 이미지들이 파일 하나로 병합됨으로 인해 얻는 이점은 다음과 같습니다.
- 다양한 환경에서 편리하게 열람할 수 있습니다.\
(이미지 파일들을 여는 것이 아닌, PDF 형태이기 때문에 스마트폰, 태블릿 등에서도 웹브라우저로 편하게 감상할 수 있습니다.)\

- 파일 관리 및 공유, 전송이 편리합니다.\
(이미지들이 들어있는 폴더를 압축하거나 할 필요 없이 PDF 파일 하나만 관리하면 됩니다.)
     

# 1. 사용법
프로그램을 작동시키기 위해서는 프로그램에게 어떤 폴더에서 작업을 진행할지 등의 옵션을 전달해야 합니다.

## 1.1. 옵션?



프로그램이 작업을 시작하기 위해서는 프로그램에 옵션을 전달해야 합니다.   
옵션들에는 필수 옵션과 선택 옵션 두 가지 종류가 있습니다.    
필수 옵션은 말 그대로 작업을 시작하기 위해 반드시 정의되어야 하는 옵션이고, 선택 옵션은 정의하지 않으면 기본값이 자동으로 정해지는 옵션입니다. 

### 1.1.1. 옵션 목록

프로그램에 전달할 수 있는 옵션들은 다음과 같습니다.

|필수|옵션명|수동 모드|자동 모드|제공 예시|
|:----:|:------:|:-----:|:-----:|:-----------:|
|○|입력 폴더|○|○|E:\WorkingSpace\Input|
|○|출력 폴더|○|○|E:\WorkingSpace\OutPut|
|X|제목 숨김|○|○|-|
|X|최종 질문|○|X|-|

### 1.1.2. 각 옵션에 대하여

먼저 아래와 같이 이루어져 있다고 가정합시다.
```bash
E:\
└─WorkingSpace
   │      merginto.exe
   │
   ├─Input
   │  ├─만화_2
   │  │      001.png
   │  │      002.png
   │  │      003.png
   │  │
   │  └─작품_1
   │          01.jpg
   │          02.jpg
   │          03.jpg
   │          04.jpg
   │
   └─OutPut
```
E드라이브 안의 `WorkingSpace`라는 폴더 안에 `Input`라는 폴더가 있고, 그 안에는 두 개의 작품 폴더가 있네요. 또 그 폴더들의 안에는 각각 3개, 4개의 작품 이미지 파일들이 들어 있군요.   
여기서 작업을 진행한다고 가정해 봅시다.   



#### 1.1.2.1. 입력 폴더

작품 폴더들이 들어있는 폴더의 경로입니다.   
헷갈릴 수 있는데, 이미지 파일들이 들어 있는 폴더가 아니라, 
이미지 파일들이 들어 있는 "작품 폴더" 들이 들어있는 폴더를 지정해주셔야 합니다.   
그러니까 우리는 이미지 파일이 있는 `만화_2` 폴더가 아니라, 모든 작품 폴더들이 모여 있는 `Input` 폴더를 `입력 폴더`로 생각해야 한다는 뜻이죠.   
해당 폴더의 경로는 `E:\WorkingSpace\Input`이 되겠죠.

*프로그램, 즉 `merginto.exe`와 같은 경로에 `Input`이라는 이름을 가진 폴더가 있다면 자동으로 선택됩니다.*



#### 1.1.2.2. 출력 폴더

병합된 PDF 파일이 출력될 폴더의 경로입니다.    
만약 출력 폴더를 `E:\WorkingSpace\OutPut` 라고 정의하였다면, 모든 작업이 끝나면 아래와 같이 될 것입니다.
```bash
E:\
└─WorkingSpace
   │      merginto.exe
   │
   ├─Input
   │  ├─만화_2
   │  │      001.png
   │  │      002.png
   │  │      003.png
   │  │
   │  └─작품_1
   │          01.jpg
   │          02.jpg
   │          03.jpg
   │          04.jpg
   │
   └─OutPut
        만화_2.pdf
        작품_1.pdf
```

*프로그램, 즉 merginto.exe와 같은 경로에 `OutPut`이라는 이름을 가진 폴더가 있다면 자동으로 선택됩니다.*



#### 1.1.2.3. 제목 숨김

작업이 진행 중일 때, 검은 콘솔 창에 현재 진행 상황을 표시하는 로그가 출력이 됩니다.   
이 때, 작품의 이름. 즉 `만화_2`, `작품_1`과 같은 작품의 이름을 다른 형태로 숨겨서 표시할지의 여부입니다.
이 옵션을 `예`로 설정할 경우 `COMIC_001`과 같이 번호를 매겨서 표시하게 됩니다. 만약 수백~수천 개의 작업을 진행한다면 오랜 시간 콘솔창이 열려 있을 텐데, 이 때 작품 혹은 문서의 이름을 숨기고 싶을 때 사용하시면 됩니다.   
물론 생성된 pdf 파일엔 숨겨진 이름이 아닌 원래 이름이 사용되죠.   
이 옵션은 프로그램 실행 후 `입력 폴더`와 `출력 폴더`가 모두 정의된 후에 사용자에게 질문을 하게 됩니다.

#### 1.1.2.4. 최종 질문 *\*해당 옵션은 `수동 모드`에서만 사용할 수 있습니다*

해당 옵션은 `merginto`를 이용해 자동 작업을 하고 싶으신 분들을 위해 추가한 옵션입니다. 모든 옵션 설정을 마친 후, 프로그램은 사용자에게 이 작업을 진행할지 마지막으로 묻습니다. 아래와 같이 말이죠.

```
2 개의 작품들을 (이미지 9개) 2개의 PDF 파일로 변환하여 위의 경로에 생성할까요?

   ...중략...

Convert 2 cartoon image folders (9 images) in a into PDF files and create them in this folder?
(Y/N) :
```
이 질문을 건너뛰고 바로 진행할지의 여부입니다. 프로그램을 더블클릭하여 실행하는 것이 아닌, cmd 등의 터미널에서 Arguments와 함께 실행할 때만, 즉 `수동 모드`일때만 사용이 가능합니다.


## 1.2. 옵션을 제공하는 법

옵션이 뭔지는 알았는데, 이제 어떻게 옵션을 제공하는 법을 알아야겠죠? 원래대로라면 
```
E:\WorkingSpace\merginto.exe /i E:\WorkingSpace\Input /o E:\WorkingSpace\OutPut /hidetitle
```
와 같이 복잡한 명령어를 사용해야 하지만, 여기 더 간단한 두 가지 방법이 있습니다. 더 간편하다고 생각되는 순서대로 적어두겠습니다.   
 참고로 아래의 두 방법은 모두 `자동 모드`입니다. `최종 질문` 옵션 사용이 불가능합니다.

### 1.2.1 폴더명을 수정하여 실행하는 법

merginto는 프로그램이 실행될 때, 프로그램 실행 파일 자기 자신이 있는 폴더 안의 다른 폴더들을 조사합니다.
만약 `Input`이라는 이름의 폴더가 있을 경우 해당 폴더를 자동으로 `입력 폴더`로 설정합니다.   
마찬가지로 `OutPut`이라는 이름의 폴더가 있을 경우 `출력 폴더`로 설정하죠. *대소문자는 상관 없습니다.*   
따라서 우리는 두 가지 방법을 생각해 볼 수 있습니다.
- 프로그램과 같은 경로에 Input이라는 이름의 폴더 생성 후 안에 작품 폴더들을 넣기
- 작품 폴더들이 들어 있는 폴더의 이름을 Input으로 바꾸고, 해당 경로에 프로그램을 옮긴 후 실행하기   

 만약 작품의 수가 매우 많다면, 두 번째 방법이 편할 것입니다.