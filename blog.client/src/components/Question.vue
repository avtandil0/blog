<template>
  <v-container>
    <v-card class="mx-auto" outlined>
      <v-list-item three-line>
        <v-list-item-content>
          <div class="overline mb-4">{{question.dateCreated | dateToDayMonthYear}}</div>
          <v-list-item-title
            class="headline mb-1"
            @click="goTo()"
            style="cursor:pointer"
          >{{question.title}}?</v-list-item-title>
          <v-list-item-subtitle>{{question.description}}</v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>

      <v-card-actions>
        <v-btn color="orange" text @click="goTo()">სრულად</v-btn>
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
  props: ['question'],
  data: () => ({}),
  created(){
      console.log('question', this.question)

  },
  methods: {
    goTo() {
      this.$router.push({ path: "Question/" + 45 });
    }
  }
};
</script>
