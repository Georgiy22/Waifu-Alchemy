mergeInto(LibraryManager.library, {

  AuthExtern: function () {
    auth();
  },

  RateGame: function () {
    ysdk.feedback.canReview()
    .then(({ value, reason }) => {
      if (value) {
        ysdk.feedback.requestReview()
        .then(({ feedbackSent }) => {
          console.log(feedbackSent);
        })
      } else {
        console.log(reason)
      }
    })
  },

  SaveExtern: function(date){
    if (player) {
        var dateString = UTF8ToString(date);
        var myobj = JSON.parse(dateString);
        player.setData(myobj);
    }
},

  LoadExtern: function(){
    initPlayer().then(_player => {
      if (_player) {
        _player.getData().then(_date=> {
          const myJSON = JSON.stringify(_date);
          myGameInstance.SendMessage('Progress', 'SetPlayerInfo', myJSON);
        });
        if (_player.getMode() !== 'lite') {
          myGameInstance.SendMessage("Yandex", "OnCheckAuth");
        }
      }
    }).catch(err => {
        // Ошибка при инициализации объекта Player.
      });
  },


  SetToLeaderboard: function(value){
    ysdk.getLeaderboards()
    .then(lb => {
      lb.setLeaderboardScore('OpenedElements', value);
    });
  },

  GetLang : function(){
      var lang = ysdk.environment.i18n.lang;
      var bufferSize = lengthBytesUTF8(lang) + 1;
      var buffer = _malloc(bufferSize);
      stringToUTF8(lang, buffer, bufferSize);
      return buffer;
  },

  ShowAdv: function(){
    ysdk.adv.showFullscreenAdv({
      callbacks: {
        onClose: function(wasShown) {
          myGameInstance.SendMessage("MusicManager", "AdMute");
        },
        onError: function(error) {
          myGameInstance.SendMessage("MusicManager", "AdMute");
        }
      }
    })
  },

  SimplifyExtern : function(){
    ysdk.adv.showRewardedVideo({
      callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
        },
        onRewarded: () => {
          console.log('Rewarded!');
          myGameInstance.SendMessage("Yandex", "SimplifyReward");
        },
        onClose: () => {
          myGameInstance.SendMessage("MusicManager", "AdMute");
        }, 
        onError: (e) => {
          myGameInstance.SendMessage("MusicManager", "AdMute");
        }
      }
    })
  },

});