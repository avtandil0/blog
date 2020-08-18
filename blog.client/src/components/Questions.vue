<template>
  <v-container>
    <div v-if="!grid">
      <div v-for="question in questions" :key="question.id">
        <Question :question="question" />
      </div>
    </div>
    <v-row v-if="grid">
      <v-col cols="12" md="4" v-for="question in questions" :key="question.id">
        <Question :question="question" />
      </v-col>
    </v-row>
    <br />
    <!-- <div class="text-center">
      <v-pagination v-model="page" :length="6"></v-pagination>
    </div>-->

    <v-btn bottom color="orange" dark fab fixed right @click="dialog = !dialog">
      <v-icon>mdi-plus</v-icon>
    </v-btn>
    <v-dialog v-model="dialog" width="800px">
      <v-card>
        <v-card-title class="yellow darken-3">შეკითხვის გაგზავნა</v-card-title>
        <v-container>
          <v-row class="mx-2">
            <v-col class="align-center justify-space-between" cols="12">
              <v-row align="center" class="mr-0">
                <v-avatar size="40px" class="mx-3">
                  <img src="//ssl.gstatic.com/s2/oz/images/sge/grey_silhouette.png" alt />
                </v-avatar>
                <v-text-field
                  placeholder="სახელი"
                  color="orange darken-3"
                  v-model="question.authorName"
                ></v-text-field>
              </v-row>
            </v-col>
            <v-col cols="12">
              <v-text-field label="სათაური" v-model="question.title" outlined></v-text-field>
            </v-col>

            <v-col cols="12">
              <v-textarea
                outlined
                label="შეკითხვა"
                color="orange darken-3"
                v-model="question.description"
              ></v-textarea>
            </v-col>
          </v-row>
        </v-container>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn text color="orange darken-3" @click="send">გაგზავნა</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-container>
</template>

<script>
import Question from "../components/Question.vue";

export default {
  name: "Questions",
  components: {
    Question
  },
  data: () => ({
    questions: [],
    question: {
      authorName: "",
      title: "",
      description: ""
    },
    dialog: false
  }),
  computed: {
    grid() {
      console.log("this.$store.state.grid", typeof this.$store.state.grid);
      return this.$store.state.grid;
    }
  },
  async created() {
    scroll(0, 0);

    const result = await this.$http.getQuestions({
      pageIndex: 1
    });
    this.questions = result.items;

    console.log("result", this.questions);
  },
  methods: {
    async send() {
      console.log("aa", this.question);
      const result = await this.$http.addQuestion(this.question);

      this.$toast(result.message, {
        color: result.isSuccess ? "green" : "red",
        dismissable: true
      });

      this.dialog = false;
      console.log("result", result);
    }
  }
};
</script>
