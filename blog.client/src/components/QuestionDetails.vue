<template>
  <v-container>
    <v-card class="mx-auto" outlined>
      <v-list-item three-line>
        <v-list-item-content>
          <v-list-item-title class="headline mb-1">{{question.title}}</v-list-item-title>
          <hr />
          <br />
          <v-list-item>{{question.description}}</v-list-item>
        </v-list-item-content>
      </v-list-item>
      <v-card-actions>
        <v-list-item class="grow">
          <v-list-item-avatar color="grey darken-3">
            <img src="//ssl.gstatic.com/s2/oz/images/sge/grey_silhouette.png" alt />
          </v-list-item-avatar>

          <v-list-item-content>
            <v-list-item-title>{{question.authorName}}</v-list-item-title>
            <span class="caption">{{question.dateCreated | dateToDayMonthYear}}</span>
          </v-list-item-content>

          <v-row align="center" justify="end">
            <v-icon class="mr-1" color="red lighten-1">mdi-heart</v-icon>
            <span class="subheading mr-2">256</span>
            <v-icon class="mr-1" color="green">mdi-share-variant</v-icon>
            <span class="subheading">45</span>
          </v-row>
        </v-list-item>
      </v-card-actions>
    </v-card>
    <br />
    <v-container v-if="currentUser">
      <html-textarea @input="updateHTML"></html-textarea>
      <br />
      <v-btn outlined color="indigo" @click="addComment">დამატება</v-btn>
    </v-container>

    <br />

    <br />
    <br />
    <v-row v-for="item in question.answers" :key="item.id">
      <v-col cols="12">
        <v-list-item>
          <v-list-item-avatar>
            <v-img src="//ssl.gstatic.com/s2/oz/images/sge/grey_silhouette.png"></v-img>
          </v-list-item-avatar>
          <v-list-item-content>
            <v-list-item-title>{{item.userName}}</v-list-item-title>
            <v-list-item-subtitle>{{item.userProfession}}</v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
      </v-col>
      <v-col cols="10">
        <div v-if="item.id == editedId">
          <html-textarea @input="updateHTML" :text="item.text"></html-textarea>
        </div>
        <div v-else class="ql-editor" v-html="item.text"></div>
      </v-col>
      <v-col cols="2">
        <v-card-actions>
          <v-btn
            v-if="currentUser && item.id == editedId"
            color="primary"
            fab
            small
            dark
            @click="editComment(item)"
          >
            <v-icon>mdi-comment-check</v-icon>
          </v-btn>
          <v-btn
            v-if="currentUser && item.id != editedId"
            color="primary"
            fab
            x-small
            dark
            @click="OpenEditableAnswer(item.id)"
          >
            <v-icon>mdi-pencil</v-icon>
          </v-btn>
          <v-btn
            v-if="currentUser && item.id != editedId"
            color="red"
            fab
            x-small
            dark
            @click="OpenDeleteAnswerModal(item.id)"
          >
            <v-icon>mdi-delete</v-icon>
          </v-btn>
        </v-card-actions>
      </v-col>
    </v-row>
    <br />
    <br />
    <br />
    <v-dialog v-model="deleteAnswerModal.open" max-width="290">
      <v-card>
        <v-card-title class="headline red lighten-2">წაშლა</v-card-title>
        <br />
        <v-card-text>
          <h3>გსურთ პასუხის წაშლა?</h3>
        </v-card-text>

        <v-card-actions>
          <v-spacer></v-spacer>

          <v-btn color="green darken-1" text @click="deleteAnswerModal.open = false">არა</v-btn>

          <v-btn color="red darken-1" text @click="deleteAnswer">დიახ</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-container>
</template>

<script>
import HtmlTextarea from "../components/HtmlTextarea.vue";

export default {
  name: "QuestionDetails",
  components: {
    HtmlTextarea
  },
  props: ["id"],
  data: () => ({
    editedId: null,
    currentUser: null,
    dialog: false,
    loading: false,
    comment: "",
    deleteAnswerModal: {
      open: false,
      id: null
    },
    question: {
      title: null,
      description: null,
      dateCreated: null,
      answers: []
    }
  }),
  methods: {
    async editComment(item) {
      const result = await this.$http.updateAnswer({
        id: item.id,
        questionID: Number(this.id),
        text: this.comment,
        userName: this.currentUser.firstName,
        userProfession: this.currentUser.lastName,
        dateCreated: item.dateCreated
      });

      this.$toast(result.message, {
        color: result.isSuccess ? "green" : "red",
        dismissable: true
      });

      if (result.isSuccess) {
        this.editedId = null;
        const result = await this.$http.getQuestionById(this.id);
        this.question = result;
        return;
      }
    },
    OpenEditableAnswer(id) {
      console.log("aaaaaaaa11");
      this.editedId = id;
    },
    async deleteAnswer() {
      const result = await this.$http.deleteAnswer(this.deleteAnswerModal.id);

      this.$toast(result.message, {
        color: result.isSuccess ? "green" : "red",
        dismissable: true
      });

      if (result.isSuccess) {
        this.deleteAnswerModal.id = null;
        this.deleteAnswerModal.open = false;
        const result = await this.$http.getQuestionById(this.id);
        this.question = result;
        return;
      }

      console.log("result", result);
    },
    async OpenDeleteAnswerModal(id) {
      this.deleteAnswerModal.id = id;
      this.deleteAnswerModal.open = true;
    },
    async addComment() {
      if (this.comment.length === 0) {
        this.$toast("დაამატეთ კომენტარო", {
          color: "red",
          dismissable: true
        });

        return;
      }
      this.loading = true;
      console.log(this.comment);
      const result = await this.$http.addAnswer({
        questionID: Number(this.id),
        text: this.comment,
        userName: this.currentUser.firstName,
        userProfession: this.currentUser.lastName
      });

      const resultD = await this.$http.getQuestionById(this.id);
      this.question = resultD;

      this.loading = false;
      console.log("result", result);
    },
    updateHTML(text) {
      console.log("text", text);
      this.comment = text;
    }
  },
  async created() {
    scroll(0, 0);
    this.currentUser = JSON.parse(localStorage.getItem("currentUser"));
    console.log("props", this.id, this.question);
    const result = await this.$http.getQuestionById(this.id);
    this.question = result;
    console.log("result", result);
  }
};
</script>
