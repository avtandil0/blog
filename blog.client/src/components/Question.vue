<template>
  <v-container>
    <v-card class="mx-auto" outlined>
      <v-list-item three-line>
        <v-list-item-content>
          <div>
            <v-toolbar :elevation="0" height="39px">
              <div class="overline mb-4">{{question.dateCreated | dateToDayMonthYear}}</div>
              <v-spacer></v-spacer>
              <v-checkbox v-if="currentUser" @change="changeStatus" v-model="question.status" color="green" label="გამოქვეყნება"></v-checkbox>
            </v-toolbar>
          </div>
          <v-list-item-title
            class="headline mb-1"
            @click="goTo(question.id)"
            style="cursor:pointer"
          >{{question.title}}?</v-list-item-title>
          <v-list-item-subtitle>{{question.description}}</v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>

      <v-card-actions>
        <v-btn color="primary" text @click="goTo(question.id)">სრულად</v-btn>
        <v-row align="center" justify="end" style="padding-right:15px">
          <v-icon class="mr-1" color="red lighten-1">mdi-heart</v-icon>
          <span class="subheading mr-2">256</span>
          <v-icon class="mr-1" color="green">mdi-share-variant</v-icon>
          <span class="subheading">45</span>
        </v-row>
      </v-card-actions>
    </v-card>
  </v-container>
</template>

<script>
export default {
  name: "Question",
  props: ["question"],
  data: () => ({
    checkbox: true,
    currentUser: null,
  }),
  created() {
    console.log("question", this.question);
    this.currentUser = JSON.parse(localStorage.getItem("currentUser"));
  },
  methods: {
    async changeStatus(){
      var result = await this.$http.changeStatus(this.question.id);

      this.$toast(result.message, {
        color: result.isSuccess ? "green" : "red",
        dismissable: true
      });

    },
    goTo(id) {
      this.$router.push({ path: "Question/" + id });
      // this.$router.push({
      //   name: "QuestionDetails",
      //   params: { id: id, question: this.question }
      // });
    }
  }
};
</script>
